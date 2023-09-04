using EmailSender.Application.Interfaces;
using EmailSender.Domain;
using EmailSender.Domain.Responses;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace EmailSender.Application.Services;

public class RabbitMQMessageService : IMessageService
{
    private readonly IOptionsMonitor<RabbitMqOptions> _rabbitmqOptions;
    private readonly IConnection _connection;

    public RabbitMQMessageService(IOptionsMonitor<RabbitMqOptions> rabbitmqOptions, IRabbitMqService rabbitMqService)
    {
        _rabbitmqOptions = rabbitmqOptions;
        _connection = rabbitMqService.AmqpConnection;
    }
    public async Task<MessageServiceResponse> SendMessage(string message)
    {
        string queueRoutingKey = _rabbitmqOptions.CurrentValue.QueueName!;
        using IModel channel = _connection.CreateModel();

        IBasicProperties properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        //string json = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(message);

        await Task.Run(() =>
        {
            channel.BasicPublish(exchange: string.Empty, routingKey: queueRoutingKey, body: body);
        });

        MessageServiceResponse messageServiceResponse = new()
        {
            Sended = true
        };

        return messageServiceResponse;
    }
}
