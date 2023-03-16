using MediatR;

namespace ActivityService.Application.Features.Activities.Commands.DeleteActivity;

public record DeleteActivityCommand(
    Guid Id) : IRequest;
