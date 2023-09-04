using EmailSender.Application.Interfaces;
using EmailSender.Domain;
using EmailSender.Domain.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailSender.Worker;

public class RabbitMQWorker : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IWorkerService _workerService;
    private readonly IOptionsMonitor<RabbitMqOptions> _rabbitMqOptions;

    public RabbitMQWorker(IRabbitMqService rabbitMqService, IWorkerService workerService, IOptionsMonitor<RabbitMqOptions> rabbitMqOptions)
    {
        _connection = rabbitMqService.AmqpConnection;
        _workerService = workerService;
        _rabbitMqOptions = rabbitMqOptions;
        _channel = CreateChannel(_connection);
    }

    internal IModel CreateChannel(IConnection connection)
    {
        string queueType = _rabbitMqOptions.CurrentValue.QueueType ?? string.Empty;
        IModel channel = connection.CreateModel();
        Dictionary<string, object> args = new()
            {
                { "x-queue-type", queueType}
            };
        channel.QueueDeclare(queue: _rabbitMqOptions.CurrentValue.QueueName,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: args);

        return channel;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
            _channel.Close();

        if (_connection.IsOpen)
            _connection.Close();

        GC.SuppressFinalize(this);

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            _connection.Dispose();
            return Task.CompletedTask;
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            try
            {
                string content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var queueMessage = JsonConvert.DeserializeObject<RootClients>(content);

                await HandleMessage(queueMessage, stoppingToken);

                _channel?.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        };

        _channel.BasicConsume(_rabbitMqOptions.CurrentValue.QueueName, false, consumer);
        return Task.CompletedTask;
    }

    internal async Task<Task> HandleMessage(RootClients? queueMessage, CancellationToken cancellationToken)
    {
        try
        {
            if (queueMessage is not null)
            {
                foreach (var client in queueMessage.Clients.Client)
                {
                    await _workerService.HandleMessage(client, cancellationToken);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Task.CompletedTask;
    }

}


