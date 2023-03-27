namespace EventsService.Infrastructure.RabbitMq;

public class RabbitMqSettings
{
    public const string SectionName = "RabbitMqSettings";

    public string HostName { get; set; } = null!;

    public string CommonQueueName { get; set; } = null!;
}
