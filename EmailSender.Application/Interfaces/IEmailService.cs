namespace EmailSender.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string message);
}
