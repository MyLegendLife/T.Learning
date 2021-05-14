using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Mq.Receive1
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
                ClientProvidedName = "R1"
            };
            // 创建一个链接
            using var connection = factory.CreateConnection();
            // 创建一个通道
            using var channel = connection.CreateModel();
            #endregion

            while (true)
            {
                var result = channel.BasicGet("Queue1", true);
                if (result != null)
                {
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
