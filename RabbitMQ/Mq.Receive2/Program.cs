using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Mq.Receive2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region

            var factory = new ConnectionFactory()
            {
                HostName = "192.168.0.28",
                UserName = "admin",
                Password = "admin",
                ClientProvidedName = "R2"
            };
            // 创建一个链接
            using var connection = factory.CreateConnection();

            // 创建一个通道
            using var channel = connection.CreateModel();

            #endregion

            while (true)
            {
                var result = channel.BasicGet("Queue2", true);
                if (result != null)
                {
                    //channel.BasicReject(result.DeliveryTag,true);

                    //var consumer = new DefaultBasicConsumer(channel);
                    //var dfd = channel.BasicConsume("queue", true, consumer);


                    var msg = Encoding.UTF8.GetString(result.Body.ToArray());
                    Console.Write($"{msg} \n");
                    //channel.BasicReject(result.DeliveryTag, true);

                    //确认
                    //channel.BasicAck(result.DeliveryTag, true);
                }
                

                Thread.Sleep(1000);
            }
        }
    }
}
