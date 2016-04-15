using IIM.Models.DAL;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IIM.Infrastucture;
using IIM.Models;

namespace IIM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(ApplicationUser), new ApplicationUserModelBinder());

            Database.SetInitializer(new IIMInitializer());
            IIMContext db = new IIMContext();
            db.Database.Initialize(true);
        }
    }
}
