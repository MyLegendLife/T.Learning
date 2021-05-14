using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace T.EventBus.RabbitMQ
{
    public interface IRabbitMqConnection : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsConnected { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool TryConnect();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}