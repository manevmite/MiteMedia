using RabbitMQ.Client;

namespace EmailSender.Application.Interfaces;

public interface IRabbitMqService
{
    IConnection AmqpConnection { get; }
}
