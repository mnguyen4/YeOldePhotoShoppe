using System.Web;
using System.Web.Mvc;
using yops_ws.Filters;

namespace yops_ws
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}