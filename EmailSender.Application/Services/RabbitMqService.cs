using EmailSender.Application.Interfaces;
using EmailSender.Domain;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EmailSender.Application.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly IOptionsMonitor<RabbitMqOptions> _configuration;
    private readonly IConnection _amqpConnection;
    public RabbitMqService(IOptionsMonitor<RabbitMqOptions> options)
    {
        _configuration = options;
        _amqpConnection = CreateAmqpConnection();
    }

    public IConnection AmqpConnection => _amqpConnection;

    public IConnection CreateAmqpConnection()
    {
        ConnectionFactory connection = new()
        {
            UserName = _configuration.CurrentValue.Username,
            Password = _configuration.CurrentValue.Password,
            HostName = _configuration.CurrentValue.HostName,
            DispatchConsumersAsync = true
        };

        IConnection amqpConnection = connection.CreateConnection();
        return amqpConnection;
    }
}
