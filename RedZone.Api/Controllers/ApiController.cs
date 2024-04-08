using RedZone.api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RedZone.api.Controllers
{
    [Authorize]
    public class ApiController : ControllerBase
    {
        public IActionResult Problem(List<Error>errors)
        {
            if(errors.Count == 0)
            {
                return Problem();
            }
            if(errors.All(x=>x.Type == ErrorType.Validation))
            {
               return ValidationProblem(errors);
            }
            HttpContext.Items[HttpContextKeys.Errors] = errors;
            var firstError = errors[0];
            return Problem(firstError);
        }

        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelState);
        }

        private IActionResult Problem(Error firstError)
        {
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status412PreconditionFailed,
                ErrorType.Unexpected => throw new NotImplementedException(),
                ErrorType.Validation => throw new NotImplementedException(),
                ErrorType.NotFound => throw new NotImplementedException(),
                ErrorType.Unauthorized => throw new NotImplementedException(),
                ErrorType.Forbidden => throw new NotImplementedException()
            };
            return Problem(detail: firstError.Description, statusCode: statusCode);
        }

    }
}
