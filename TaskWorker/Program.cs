using Microsoft.EntityFrameworkCore;
using TaskWorker;
using TaskWorker.Data;
using TaskWorker.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Rabbit MQ server
builder.Services.AddRabbitMQService(builder.Configuration);

builder.Services.AddDbContext<TaskManagementContext>(options => options.UseInMemoryDatabase("TaskManagement"));

var host = builder.Build();
host.Run();
