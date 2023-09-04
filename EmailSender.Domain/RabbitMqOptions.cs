namespace EmailSender.Domain;

public class RabbitMqOptions
{
    public const string Section = "RabbitMq";
    public string? HostName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? QueueName { get; set; }
    public string? QueueType { get; set; }
}