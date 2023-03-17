using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using FluentValidation;

namespace EventService.Application.Features.Tickets.Commands.IssueTicketToUser;

internal class IssueTicketToUserCommandValidator : AbstractValidator<IssueTicketToUserCommand>
{
    public IssueTicketToUserCommandValidator(IRepository<Event> eventRepository)
    {
        RuleFor(x => x.EventId).Must(x => eventRepository.GetEntity(x).HasFreePlaces)
            .WithMessage("No free places");
    }
}
