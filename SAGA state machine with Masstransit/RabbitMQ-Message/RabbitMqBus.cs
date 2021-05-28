
using MassTransit;
using MassTransit.RabbitMqTransport;
using RabbitMq.Constants;
using System;

namespace rabbitmq.Constants
{
    public class RabbitMqBus
    {
        public static MassTransit.IBusControl ConfigureBus(IServiceProvider provider, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
         registrationAction = null)
        {
            return MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(BusConstants.RabbitMqUri), hst =>
                {
                    hst.Username(BusConstants.UserName);
                    hst.Password(BusConstants.Password);
                });

                cfg.ConfigureEndpoints(provider);

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}