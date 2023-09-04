using EmailSender.Domain.Responses;

namespace EmailSender.Application.Interfaces;

public interface IMessageService
{
    Task<MessageServiceResponse> SendMessage(string message);
}
