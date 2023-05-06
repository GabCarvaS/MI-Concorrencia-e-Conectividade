using System.Web;
using System.Web.Mvc;

namespace WebApi_PBL_Pacientes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
