using System.Web;
using System.Web.Mvc;

namespace _19_ADO_Assignment_02
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
