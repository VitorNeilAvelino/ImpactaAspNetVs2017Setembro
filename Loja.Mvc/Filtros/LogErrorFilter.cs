using System.Web.Mvc;

namespace Loja.Mvc.Filtros
{
    public class LogErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var controller = filterContext.RouteData
                                    .Values["controller"].ToString();
            var action = filterContext.RouteData
                                    .Values["action"].ToString();
            var loggerName = $"{controller}Controller.{action}";

            log4net.LogManager.GetLogger(loggerName)
                .Error(filterContext.Exception.Message, filterContext.Exception);

            base.OnException(filterContext);    
        }
    }
}