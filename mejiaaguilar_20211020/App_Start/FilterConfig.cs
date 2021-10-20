using System.Web;
using System.Web.Mvc;

namespace mejiaaguilar_20211020
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
