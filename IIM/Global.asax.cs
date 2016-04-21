using IIM.Models.DAL;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IIM.Infrastucture;
using IIM.Models;
using IIM.ViewModels.ReservationViewModels;

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
            ModelBinders.Binders.Add(typeof(ReservationDateRangeViewModel), new ReservationDateRangeBinder());

            Database.SetInitializer(new IIMInitializer());
            IIMContext db = new IIMContext();
            db.Database.Initialize(true);
        }
    }
}
