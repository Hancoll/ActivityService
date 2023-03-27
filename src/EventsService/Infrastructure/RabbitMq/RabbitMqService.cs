﻿using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EventsService.Infrastructure.RabbitMq;

public class RabbitMqService : IRabbitMqService
{
    private readonly string _hostName;
    private readonly string _commonQueueName;

    public RabbitMqService(IOptions<RabbitMqSettings> options)
    {
        _hostName = options.Value.HostName;
        _commonQueueName = options.Value.CommonQueueName;
    }

    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _commonQueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: _commonQueueName,
            basicProperties: null,
            body: body);
    }
}
