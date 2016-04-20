using IIM.ViewModels;
using IIM.ViewModels.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIM.Models;
using IIM.Models.Domain;
using WebGrease.Css.Extensions;

namespace IIM.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMaterialRepository _materialRepository;

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
            ViewBag.disabled = true;
            return View(new NewReservationViewModel(DateTime.Today, DateTime.Today, user.WishList.Materials.Select(m => new ReservationDetailSelectionViewModel(m, 0, 0)), user.Type));
        }

        [HttpPost]
        public ActionResult ChangeReservationRange(DateTime startDate, DateTime endDate, ApplicationUser user)
        {
            /*
            var startDateRestrictions = AppSettings.GetStartDateRangeRestriction(user.Type);
            if (startDateRestrictions.DefaultRestrictionType == DateTimeRestriction.RestrictionType.Allow && startDateRestrictions?.Restrictions.Any(r => r.IsValid(startDate)))
            {

            }*/
            ViewBag.disabled = false;
            //if (Request.IsAjaxRequest())
            //{
            //    return Json(
            //        user.WishList.Materials.Select(
            //            m => new ReservationDetailSelectionViewModel(m,
            //                                _reservationRepository.GetAmountOfAvailableIdentifiers(startDate, endDate, m),
            //                                0)),JsonRequestBehavior.AllowGet);
            //}

            return View("Create", new NewReservationViewModel(startDate, endDate, user.WishList.Materials.Select(
                        m => new ReservationDetailSelectionViewModel(m,
                                            _reservationRepository.GetAmountOfAvailableIdentifiers(startDate, endDate, m),
                                            0)), user.Type));
        }

        public ActionResult CreateReservation(NewReservationViewModel newReservationModel, ApplicationUser user)
        {
            Reservation res = user.CreateReservation(newReservationModel.StartDate, newReservationModel.EndDate);
            foreach (var material in newReservationModel.TheMaterials)
            {
                List<ReservationDetail> details =
                    _reservationRepository.GetAvailableIdentifiers(
                        res.StartDate,
                        res.EndDate,
                        material.RequestedAmount,
                        _materialRepository.FindById(material.TheMaterial.Id))
                            .Select(mi => new ReservationDetail(res, mi)).ToList();
                res.AddAllDetails(details);
            }

            return View("Index");
        }

    }
}