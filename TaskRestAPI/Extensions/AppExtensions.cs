using MassTransit;
using TaskManagementEntity.BusCommands;
using TaskManagementEntity.Model;

namespace TaskRestAPI.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var configURI = configuration["RabbitMQ:URI"];
            if (string.IsNullOrEmpty(configURI))
            {
                throw new ConfigurationException("Rabbit MQ URI not set");
            }
            
            var rabbitUri = new Uri(configURI);

            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddRequestClient<TaskItemGet>();

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
    }
}
