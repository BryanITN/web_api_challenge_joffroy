using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace web_api_challenge.Exceptions
{
    public class CustomExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is JoffroyException customException)
            {
                var errorResponse = new
                {
                    Message = customException.Message,
                    Code = "Error",
                    Details = ""
                };

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
            }
            else
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrió un error en el servidor.");
            }
        }
    }
}