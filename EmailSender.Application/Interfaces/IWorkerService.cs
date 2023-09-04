using EmailSender.Domain.Models;

namespace EmailSender.Application.Interfaces;

public interface IWorkerService
{
    Task HandleMessage(Client queueMessage, CancellationToken cancellationToken);
}

