using EmailSender.Application.Interfaces;
using EmailSender.Domain.Models;

namespace EmailSender.Application.Services;
public class WorkerService : IWorkerService
{
    private readonly IEmailService _emailService;
    public WorkerService(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task HandleMessage(Client client, CancellationToken cancellationToken)
    {
        CheckConfiguration(client);
        await _emailService.SendEmailAsync(string.Empty);
    }

    private void CheckConfiguration(Client client)
    {
        //Check if client has some configurations in db
    }
}
