using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Api.RabbitMq;

namespace Ordering.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static EventBusRabbitMqConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMqConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopped.Register(OnStopped);

            return app;
        }

        public static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopped()
        {
            Listener.Disconnect();
        }
    }
}
