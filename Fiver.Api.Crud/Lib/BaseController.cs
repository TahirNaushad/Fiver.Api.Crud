using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fiver.Api.Crud.Lib
{
    public class BaseController : Controller
    {
        [NonAction]
        public UnprocessableObjectResult Unprocessable(ModelStateDictionary modelState)
        {
            return new UnprocessableObjectResult(modelState);
        }

        [NonAction]
        public ObjectResult Unprocessable(object value)
        {
            return new UnprocessableObjectResult(value);
        }
    }
}
