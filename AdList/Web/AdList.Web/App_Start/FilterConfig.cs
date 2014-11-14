namespace AdList.Web
{
    using AdList.Web.Infrastructure.Filters;
    using System.Web;
    using System.Web.Mvc;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ApplicationVersionHeaderFilter());
        }
    }
}
