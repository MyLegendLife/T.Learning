using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mq.Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.0.28",
                UserName = "admin",
                Password = "admin",
                Port = 5672,
                ClientProvidedName = "S1"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //删除
            channel.ExchangeDelete("Exchange_Direct", false);
            channel.ExchangeDelete("Exchange_Fanout", false);
            channel.ExchangeDelete("Exchange_Topic", false);
            channel.QueueDelete("Queue1");
            channel.QueueDelete("Queue2");


            //声明交换机
            channel.ExchangeDeclare("Exchange_Direct", ExchangeType.Direct);
            channel.ExchangeDeclare("Exchange_Fanout", ExchangeType.Fanout);
            channel.ExchangeDeclare("Exchange_Topic", ExchangeType.Topic);

            //声明队列
            channel.QueueDeclare(
                queue: "Queue1",
                durable: true,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            //声明队列
            channel.QueueDeclare(
                queue: "Queue2",
                durable: true,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            //队列绑定交换机
            channel.QueueBind("Queue1", "Exchange_Direct", "Routing1");
            channel.QueueBind("Queue2", "Exchange_Direct", "Routing2");
            channel.QueueBind("Queue1", "Exchange_Fanout", "Routing1");
            channel.QueueBind("Queue2", "Exchange_Fanout", "Routing2");
            channel.QueueBind("Queue1", "Exchange_Topic", "Routing1.#");
            channel.QueueBind("Queue2", "Exchange_Topic", "#.Routing2");


            //var response = channel.QueueDeclarePassive("mytestqueue");
            //var messageCount = response.MessageCount;
            //var consumerCount = response.ConsumerCount;

            // 创建一个消息
            var message = "Hello World";
            // 编码一个消息
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; //持久化
            // 发布一个消息
            channel.BasicPublish(
                exchange: "Exchange_Direct",
                routingKey: "Routing1",
                mandatory:true,
                basicProperties: properties,
                body: body
            );
            System.Console.Write("Sent Message:{0}", message);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (Send, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                channel.BasicAck(e.DeliveryTag,multiple:false);
            };

            channel.BasicConsume("Queue1", false, consumer);

            channel.CallbackException += async (sender, e) =>
            {
                throw new NotImplementedException();
            };

            Console.ReadLine();
        }
    }
}
