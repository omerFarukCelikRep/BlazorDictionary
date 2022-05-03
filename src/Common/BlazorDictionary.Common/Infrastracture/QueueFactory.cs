using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BlazorDictionary.Common.Infrastracture;

public static class QueueFactory
{
    public static void SendMessageToExhange(string exchangeName,
                                   string exchangeType,
                                   string queueName,
                                   object obj)
    {
        var channel = CreateBasicConsumer()
                                        .EnsureExchange(exchangeName, exchangeType)
                                        .EnsureQueue(queueName, exchangeName)
                                        .Model;

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

        channel.BasicPublish(exchange: exchangeName,
                             routingKey: queueName,
                             body: body);
    }

    public static EventingBasicConsumer CreateBasicConsumer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = DictionaryConstants.RabbitMqHost
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        return new EventingBasicConsumer(channel);
    }

    public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer,
                                                       string exchangeName,
                                                       string exchangeType = DictionaryConstants.DefaultExchangeType)
    {
        consumer.Model.ExchangeDeclare(exchange: exchangeName,
                                       type: exchangeType,
                                       durable: false,
                                       autoDelete: false);

        return consumer;
    }

    public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                       string queueName,
                                                       string exchangeName)
    {
        consumer.Model.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    null);

        consumer.Model.QueueBind(queue: queueName,
                                 exchange: exchangeName,
                                 routingKey: queueName);

        return consumer;
    }
}
