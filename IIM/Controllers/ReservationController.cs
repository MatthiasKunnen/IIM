using IIM.ViewModels;
using IIM.ViewModels.ReservationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IIM.Models;
using IIM.Models.Domain;
using WebGrease.Css.Extensions;

namespace IIM.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IMaterialRepository materialRepository, IReservationRepository reservationRepository)
        {
            _materialRepository = materialRepository;
            _reservationRepository = reservationRepository;
        }


        public ActionResult Index(ApplicationUser user)
        {
            return View(user.Reservations.Select(r => new ReservationViewModel(r)));
        }

        public ActionResult Create(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel)
        {
            reservationDateRangeViewModel.SetType(user.Type);
            var isDateRangeValid = true;
            Func<Material, int> func = material => 0;
            if (isDateRangeValid)
            {
                func = material =>
                    material.GetAvailableIdentifiers(reservationDateRangeViewModel.StartDate,
                        reservationDateRangeViewModel.EndDate).Count();
            }
            var materials = user.WishList.Materials.Select(m => new ReservationDetailSelectionViewModel(m, func.Invoke(m), 0));
            var materialsPicker = new NewReservationMaterialsViewModel(materials, false);
            var wrapper = new NewReservationViewModel(reservationDateRangeViewModel, materialsPicker);
            return View(wrapper);
        }

        [HttpPost]
        public ActionResult ChangeReservationRange(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, DateTime startDate, DateTime endDate)
        {
            reservationDateRangeViewModel.SetType(user.Type);
            reservationDateRangeViewModel.StartDate = startDate;
            reservationDateRangeViewModel.EndDate = endDate;
            return RedirectToAction("Create");
        }

        public ActionResult CreateReservation(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, NewReservationMaterialsViewModel reservationMaterialsViewModel)
        {
            var res = user.CreateReservation(reservationDateRangeViewModel.StartDate, reservationDateRangeViewModel.EndDate);
            foreach (var detail in reservationMaterialsViewModel.Materials)
            {
                //res.AddMaterial(detail.Material,detail.RequestedAmount);
            }

            return View("Index");
        }

       }
}