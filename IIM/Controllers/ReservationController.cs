using IIM.ViewModels;
using IIM.ViewModels.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIM.Models;
using IIM.Models.Domain;

namespace IIM.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IUserRepository userRepository, IReservationRepository reservationRepository)
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
        }


        public ActionResult Index(ApplicationUser user)
        {
            return View(user.Reservations.Select(r => new ReservationViewModel(r)));
        }

        public ActionResult Create(ApplicationUser user)
        {
            return View(user.WishList.Materials.Select(m => new ReservationDetailSelectionViewModel(m, -1, -1)));
        }

        [HttpPost]
        public ActionResult ChangeReservationRange(DateTime startDate, DateTime endDate, ApplicationUser user)
        {
            if (Request.IsAjaxRequest())
            {
                return Json(
                    user.WishList.Materials.Select(
                        m => new ReservationDetailSelectionViewModel(m,
                                            _reservationRepository.GetAmountOfAvailableIdentifiers(startDate, endDate, m),
                                            0)),JsonRequestBehavior.AllowGet);
            }

            return View("Create",user.WishList.Materials.Select(
                        m => new ReservationDetailSelectionViewModel(m,
                                            _reservationRepository.GetAmountOfAvailableIdentifiers(startDate, endDate, m),
                                            0)));
        }
    }
}