using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Net.Http;
using T.EventBus.RabbitMQ;

namespace T.EventBus.Test
{
    public static class EventBusTestExtension
    {
        /// <summary>
        /// 消息总线RabbitMQ
        /// </summary>
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            #region 加载RabbitMQ账户
            services.AddSingleton<IRabbitMqConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMqConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"]
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new RabbitMqConnection(factory, logger, retryCount);
            });
            #endregion

            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                var rabbitMqConnection = sp.GetRequiredService<IRabbitMqConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMq(rabbitMqConnection, logger, iLifetimeScope, httpClientFactory, subscriptionClientName, retryCount);
            });
        }

        //绑定RoutingKey与队列
        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app, IConfiguration configuration)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.SubscribeAsync(configuration["SubscriptionClientName"], "UserRegister");

            return app;
        }
    }
}