using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using IIM.ViewModels.ReservationViewModels;

namespace IIM.Infrastucture
{
    public class ReservationDateRangeBinder : IModelBinder
    {
        private const string SessionKey = "DateRange";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return controllerContext.HttpContext.Session == null 
                ? null 
                : controllerContext.HttpContext.Session[SessionKey] ??
                   (controllerContext.HttpContext.Session[SessionKey] =
                       new ReservationDateRangeViewModel()
                       {
                           StartDate = DateTime.Today,
                           EndDate = DateTime.Today.AddDays(4)
                       });
        }
    }
}