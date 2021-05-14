using System;
using System.Net.Sockets;
using Polly;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;

namespace Mq.Send.s
{
    public class Class1
    {
        public void df()
        {
            var policy = Policy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    //_logger.LogWarning(ex.ToString());
                });
        }
    }
}