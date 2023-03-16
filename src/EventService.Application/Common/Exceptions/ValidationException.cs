using FluentValidation.Results;
using System.Net;

namespace ActivityService.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public List<ValidationFailure> Errors { get; }

    public ValidationException(List<ValidationFailure> errors)
    {
        Errors = errors;
    }
}
