using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using API.Exceptions;

namespace API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ObjectResult result;

            var exception = context?.Exception;

            switch (exception)
            {
                case OperationException ex:
                    result = new ObjectResult(ex.Error ?? new OperationError()) { StatusCode = ex.StatusCode };
                    break;

                default:
                    result = new ObjectResult(new OperationError()) { StatusCode = StatusCodes.Status500InternalServerError };
                    break;
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
