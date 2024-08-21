using MassTransit;
using TaskManagementEntity.BusCommands;
using TaskManagementEntity.Model;
using TaskWorker.TaskManagement;
using TaskWorker.TaskManagement.Interfaces;
using TaskWorker.BusCommands;
using TaskWorker.Bus;
using Microsoft.EntityFrameworkCore;
using TaskWorker.Data;

namespace TaskWorker.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddTransient<IConsumer<TaskItemCreateOrUpdate>, TaskManagementConsumerCreateOrUpdate>();
            services.AddTransient<IConsumer<TaskItemGet>, TaskManagementConsumerGet>();
            
            services.AddScoped<ITaskManagementData, TaskManagementData>();

            var configURI = configuration["RabbitMQ:URI"];
            if (string.IsNullOrEmpty(configURI))
            {
                throw new ConfigurationException("Rabbit MQ URI not set");
            }
            
            var rabbitUri = new Uri(configURI);

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumer<TaskManagementConsumerCreateOrUpdate>();
                busConfigurator.AddConsumer<TaskManagementConsumerGet>();

                busConfigurator.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host(rabbitUri, host =>
                        {
                            // TODO configure user secret repository and management
                            host.Username("guest");
                            host.Password("guest");
                        });
                        
                        cfg.ConfigureEndpoints(ctx);
                    });
            });
        }

        public static void AddDataBaseContext(this IServiceCollection services)
        {
            services.AddScoped<ITaskManagementData, TaskManagementData>();
            services.AddDbContext<TaskManagementContext>(options => options.UseInMemoryDatabase("tasksdb"));
        }
    }
}
