using FluentValidation.Results;
using SC.Internship.Common.Exceptions;

namespace EventService.Application.Common.Exceptions;

public class ValidationException : ScException
{
    public List<ValidationFailure> Errors { get; }

    public ValidationException(List<ValidationFailure> errors) : base(null)
    {
        Errors = errors;
    }
}
