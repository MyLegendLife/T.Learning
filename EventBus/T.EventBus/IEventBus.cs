using System.Threading.Tasks;

namespace T.EventBus
{
    public interface IEventBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task PublishAsync(string routingKey, object model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="routingKey"></param>
        /// <returns></returns>
        Task SubscribeAsync(string queueName, string routingKey);
    }
}