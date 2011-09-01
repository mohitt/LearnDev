using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ads.Model.EFRepositories;
using Ads.Web.Controllers;
using Microsoft.Web.Mvc;
using Ninject;
using Ninject.Web.Mvc;

namespace Ads.Web
{
    public class AdsApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
//#if !DEBUG
  //          filters.Add(new ProdAuthorization());
//#endif
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
        
        protected void Application_Start() {
            
//#if DEBUG
            DbIntializer.CreateContext();
//#endif
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


    }

    /// <summary>
    /// This class is not needed as of now... 
    /// </summary>
    public class ProdAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Admin" && filterContext.ActionDescriptor.ActionName == "Logon")
                return;
            var currentContext = filterContext.RequestContext.HttpContext;

            if (currentContext.Session["adminSession"] != null && currentContext.Request.Cookies["adminSession"] != null && currentContext.Session["adminSession"].ToString() == currentContext.Request.Cookies["adminSession"].Value)
                return;

            currentContext.Session["redirectToUrl"] =
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "," +
                filterContext.ActionDescriptor.ActionName;
            filterContext.Result = new AdminController().RedirectToAction(ctr => ctr.Logon());


        }

        //public override void OnResultExecuting(ResultExecutingContext context) {
        //    base.OnResultExecuting(context);

        //    context.RequestContext.HttpContext.Response.Write("<!-- Buuu! -->");
        //}
    }
}