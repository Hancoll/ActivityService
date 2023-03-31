using EventsService.Features.Events;
using EventsService.Features.Events.DeleteEventsAfterSpaceDeleted;
using EventsService.Features.Events.UpdateEventsAfterImageDeleted;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EventsService.Infrastructure.RabbitMq;

public class RabbitMqListener : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IMediator _mediator;
    private readonly string _commonQueueName;

    public RabbitMqListener(IMediator mediator, IOptions<RabbitMqSettings> options)
    {
        _mediator = mediator;
        var rabbitMqSettings = options.Value;
        _commonQueueName = options.Value.CommonQueueName;

        var factory = new ConnectionFactory
        {
            HostName = rabbitMqSettings.HostName,
            UserName = rabbitMqSettings.UserName,
            Password = rabbitMqSettings.Password,
            RequestedHeartbeat = TimeSpan.FromSeconds(60)
        };

        // Создание соединения

        while (_connection is null)
        {
            try
            {
                _connection = factory.CreateConnection();
            }

            catch
            {
                Thread.Sleep(3000);
            }
        }

        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _commonQueueName, durable: false, exclusive: false, autoDelete: false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            var @event = GetEvent(content);

            switch (@event)
            {
                case null:
                    _channel.BasicReject(ea.DeliveryTag, false);
                    return;
                case SpaceDeleteEvent spaceDeleteEvent:
                    var deleteEventsAfterSpaceDeletedCommand = new DeleteEventsAfterSpaceDeletedCommand(spaceDeleteEvent.DeletedSpaceId);
                    await _mediator.Send(deleteEventsAfterSpaceDeletedCommand, stoppingToken);
                    break;
                case ImageDeleteEvent imageDeleteEvent:
                    var updateEventsAfterImageDeletedCommand = new UpdateEventsAfterImageDeletedCommand(imageDeleteEvent.DeletedImageId);
                    await _mediator.Send(updateEventsAfterImageDeletedCommand, stoppingToken);
                    break;
            }

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(_commonQueueName, false, consumer);
        return Task.CompletedTask;
    }


    private static object? GetEvent(string content)
    {
        var @event = JsonConvert.DeserializeObject<dynamic>(content)!;

        int type = @event.Type;

        object? result = type switch
        {
            1 => new SpaceDeleteEvent((Guid)@event.DeletedSpaceId),
            2 => new ImageDeleteEvent((Guid)@event.DeletedImageId),
            _ => null
        };

        return result;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
