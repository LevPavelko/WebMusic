using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebMusic.Filters
{
    public class CultureAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string? cultureName = null;
           
            var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie;
            else
                cultureName = "ru";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk", "de" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }
           
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
           
        }
    }
}
