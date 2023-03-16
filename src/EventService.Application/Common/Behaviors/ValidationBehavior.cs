using FluentValidation;
using MediatR;

namespace ActivityService.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators is null || _validators.Count() == 0)
            return await next();

        var failures = _validators
            .Select(v => v.Validate(request))
            .Where(v => !v.IsValid)
            .SelectMany(result => result.Errors);

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }

    public ValidationBehavior(IEnumerable<IValidator<TRequest>>? validators)
    {
        _validators = validators;
    }
}
