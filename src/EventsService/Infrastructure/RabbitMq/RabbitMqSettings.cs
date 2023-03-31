namespace EventsService.Infrastructure.RabbitMq;

public class RabbitMqSettings
{
    public const string SectionName = "RabbitMqSettings";

    public string HostName { get; init; } = null!;

    public string UserName { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string CommonQueueName { get; init; } = null!;
}
