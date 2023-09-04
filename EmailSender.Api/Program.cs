using EmailSender.Domain;
using EmailSender.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen()
    .AddPersistence(builder.Configuration);

builder.Services.AddOptions<RabbitMqOptions>().Bind(builder.Configuration.GetSection(RabbitMqOptions.Section));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
