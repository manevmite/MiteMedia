namespace EmailSender.Domain.Responses;

public class MessageServiceResponse
{
    public bool Sended { get; set; }
    public string? MessageId { get; set; }
    public DateTimeOffset? QueueMessageExpirationTime { get; set; }
    public DateTimeOffset? QueueMessageInsertionTime { get; set; }
    public string? QueueMessagePopReceipt { get; set; }
    public DateTimeOffset? QueueMessageTimeNextVisible { get; set; }
}
