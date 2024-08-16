using TaskWorker;
using TaskWorker.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Rabbit MQ server
builder.Services.AddRabbitMQService(builder.Configuration);

var host = builder.Build();
host.Run();
