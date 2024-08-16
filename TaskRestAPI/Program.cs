using MassTransit;
using TaskRestAPI.Extensions;
using TaskRestAPI.Business.Interfaces;
using TaskRestAPI.Business;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskManagementBusiness, TaskManagementBusiness>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Rabbit MQ server
builder.Services.AddRabbitMQService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
