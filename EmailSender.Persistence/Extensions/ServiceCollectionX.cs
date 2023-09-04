using EmailSender.Application.Interfaces;
using EmailSender.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Persistence.Extensions;

public static class ServiceCollectionX
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IMessageService, RabbitMQMessageService>();
        services.AddSingleton<IWorkerService, WorkerService>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }   
}
