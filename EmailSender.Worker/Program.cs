using EmailSender.Domain;
using EmailSender.Persistence.Extensions;
using EmailSender.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

IConfiguration Configuration = new ConfigurationManager()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", false, true)
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<RabbitMQWorker>();
        services.AddOptions<RabbitMqOptions>().Bind(Configuration.GetSection(RabbitMqOptions.Section));
        services.AddPersistence((ConfigurationManager)Configuration);
    })
    .Build();

await host.RunAsync();
