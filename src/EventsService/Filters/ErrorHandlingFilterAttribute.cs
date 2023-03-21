using EventsService.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventsService.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is not ScException)
            return;

        ObjectResult objectResult;

        if (exception is ValidationException validationException)
        {
            var modelStateDictionary = new Dictionary<string, List<string>>();

            foreach (var error in validationException.Errors)
            {
                if (!modelStateDictionary.ContainsKey(error.ErrorCode))
                    modelStateDictionary.Add(error.ErrorCode, new());

                modelStateDictionary[error.ErrorCode].Add(error.ErrorMessage);
            }

            var result = new ScResult(new ScError { Message = "Validation fault", ModelState = modelStateDictionary });
            objectResult = new ObjectResult(result);
        }
        else
            objectResult = new ObjectResult(exception.Message);

        context.Result = objectResult;
        context.ExceptionHandled = true;
    }
}
