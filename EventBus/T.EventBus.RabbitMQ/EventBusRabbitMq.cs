using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace T.EventBus.RabbitMQ
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private const string BROKER_NAME = "mi_event_bus";
        private readonly string AUTOFAC_SCOPE_NAME = "mi_event_bus";

        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly ILogger<EventBusRabbitMq> _logger;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IHttpClientFactory _httpClientFactory;
        private IModel _model;

        private string _queueName;
        private readonly int _retryCount;

        public EventBusRabbitMq(
            IRabbitMqConnection rabbitMqConnection, 
            ILogger<EventBusRabbitMq> logger, 
            ILifetimeScope lifetimeScope, 
            IHttpClientFactory httpClientFactory, 
            string queueName = null, 
            int retryCount = 5)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _logger = logger;
            _lifetimeScope = lifetimeScope;
            _queueName = queueName;
            _retryCount = retryCount;
            _model = CreateConsumerChannel();
            _httpClientFactory = httpClientFactory;

        }

        public async Task PublishAsync(string routingKey, object model)
        {
            if (!_rabbitMqConnection.IsConnected)
            {
                _rabbitMqConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetryAsync(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex.ToString());
                });

            using (var channel = _rabbitMqConnection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");
                var message = JsonConvert.SerializeObject(model);
                var body = Encoding.UTF8.GetBytes(message);

                await policy.ExecuteAsync(async () =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; //持久化

                    channel.BasicPublish(
                        exchange: BROKER_NAME, 
                        routingKey: routingKey, 
                        mandatory: true, 
                        basicProperties: properties, 
                        body: body);

                    //return Task.CompletedTask;
                });
            }
        }

        public Task SubscribeAsync(string queueName, string routingKey)
        {
            if (!_rabbitMqConnection.IsConnected)
            {
                _rabbitMqConnection.TryConnect();
            }

            using (var channel = _rabbitMqConnection.CreateModel())
            {
                channel.QueueBind(queue: queueName, exchange: BROKER_NAME, routingKey: routingKey);
            }

            return Task.CompletedTask;
        }

        private IModel CreateConsumerChannel()
        {
            if (!_rabbitMqConnection.IsConnected)
            {
                _rabbitMqConnection.TryConnect();
            }

            var channel = _rabbitMqConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            channel.QueueDeclare(
                queue: _queueName, 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                await ProcessEvent(ea.RoutingKey, message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _model.Dispose();
                _model = CreateConsumerChannel();
            };

            return channel;
        }

        /// <summary>
        /// 发送MQ到指定服务接口
        /// </summary>
        private async Task ProcessEvent(string routingKey, string message)
        {
           var client = _httpClientFactory.CreateClient("asd");

            //await异步等待回应
            //var response = await client.GetAsync("https://localhost:44302/weatherforecast");

            var httpContent = new StringContent(message, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:44302/values", httpContent);

        }

        public void Dispose()
        {
            _model?.Dispose();
        }
    }
}