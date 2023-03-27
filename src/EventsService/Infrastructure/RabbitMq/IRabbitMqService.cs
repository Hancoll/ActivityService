namespace EventsService.Infrastructure.RabbitMq;

public interface IRabbitMqService
{
    void SendMessage(object obj);
}
