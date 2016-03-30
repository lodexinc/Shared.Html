using System.Net;
using System.Text;
using System.Web.Mvc;
using log4net;

namespace Shared.Html.Validation
{
    public class ClientErrorHandler : FilterAttribute, IExceptionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ClientErrorHandler));

        public void OnException(ExceptionContext filterContext)
        {
            var error = new StringBuilder();
            var errorClientFriendly = string.Empty;


            // Log our message
            var innerMostException = filterContext.Exception;
            if (innerMostException != null)
            {
                while (innerMostException.InnerException != null)
                {
                    innerMostException = innerMostException.InnerException;
                }

                errorClientFriendly = innerMostException.Message;
                error.AppendLine(innerMostException.Message);
                error.AppendLine(innerMostException.StackTrace);
                
                Log.Error( error );    
            }

            if (!filterContext.HttpContext.Request.IsAjaxRequest() || filterContext.Exception == null)
            {
                return;
            }

            // Construct Json Error object to return
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new JsonError
                {
                    Error = errorClientFriendly
                }
            };
            filterContext.ExceptionHandled = true;
        }
    }
}

