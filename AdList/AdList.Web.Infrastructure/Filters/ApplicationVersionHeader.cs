using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdList.Web.Infrastructure.Filters
{
    public class ApplicationVersionHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("Application", "AdList");
            base.OnActionExecuted(filterContext);
        }
    }
}
