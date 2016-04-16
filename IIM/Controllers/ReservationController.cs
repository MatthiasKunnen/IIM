using IIM.ViewModels;
using IIM.ViewModels.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IIM.Controllers
{
    public class ReservationController : Controller
    {

        public ReservationController()
        {

        }
        // GET: Reservation
        public ActionResult Index()
        {
            //Pieter gmoet de reservaties doorgeven als een reservationviewmodel xxx Seghers
            ReservationsViewModel rvm = new ReservationsViewModel()
            {
                Details = null /*reservatiedetails van 1 reservatie, easy gg */
            };
            return View();
        }

        public ActionResult ShowDetails(ReservationsViewModel rvm)
        {
            IEnumerable<ReservationDetailViewModel> details = rvm.Details
                                                                .Select(detail=>new ReservationDetailViewModel(detail))
                                                                .ToList();
            
            return PartialView("Details", details);
        }
    }
}