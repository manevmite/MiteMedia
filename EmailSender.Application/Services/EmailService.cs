using EmailSender.Application.Interfaces;

namespace EmailSender.Application.Services;
public class EmailService : IEmailService
{
    public Task SendEmailAsync(string message)
    {
        throw new NotImplementedException();
    }
}
