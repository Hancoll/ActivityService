using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ActivityService.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        
        ProblemDetails problemDetails;

        if(exception is ValidationException validationException)
        {
            var modelStatieDectionary = new ModelStateDictionary();

            foreach (var error in validationException.Errors)
            {
                modelStatieDectionary.AddModelError(error.ErrorCode, error.ErrorMessage);
            }

            problemDetails = new ValidationProblemDetails(modelStatieDectionary);
        }

        else
            problemDetails = new ProblemDetails { Detail = exception.Message};

        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}
