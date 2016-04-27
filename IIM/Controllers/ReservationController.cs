using IIM.ViewModels.ReservationViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using IIM.Models;
using IIM.Models.Domain;
using System.Collections.Generic;

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
        public ActionResult ChangeReservationRange(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, DateTime startDate, DateTime? endDate)
        {
            reservationDateRangeViewModel.SetType(user.Type);
            reservationDateRangeViewModel.StartDate = startDate;
            reservationDateRangeViewModel.EndDate = endDate ?? GetNextWeekday(startDate, DayOfWeek.Friday);
            return RedirectToAction("Create");
        }

        public ActionResult CreateReservation(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, NewReservationMaterialsViewModel reservationMaterialsViewModel)
        {
            Dictionary<Material, int> requestedMaterials = new Dictionary<Material, int>();
            foreach (var detail in reservationMaterialsViewModel.Materials)
            {
                requestedMaterials.Add(_materialRepository.FindById(detail.Material.Id), detail.RequestedAmount);
            }

            user.CreateReservation(reservationDateRangeViewModel.StartDate, reservationDateRangeViewModel.EndDate, requestedMaterials);
            _reservationRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            var result = start.AddDays(1);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }
    }
}