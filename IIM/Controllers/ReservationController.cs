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
        /*
        public ActionResult Create(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel)
        {
            reservationDateRangeViewModel.SetType(user.Type);
            
            return Create(new NewReservationViewModel(reservationDateRangeViewModel, 
                new NewReservationMaterialsViewModel(user.WishList.Materials.Select(m => new ReservationDetailSelectionViewModel(m, 0, 0)))));
        }
        */
        public ActionResult Create(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel)
        {
            var isDateRangeValid = true;
            Func<Material, int> func = material => 0;
            if (isDateRangeValid)
            {
                func = material =>
                        _reservationRepository.GetAmountOfAvailableIdentifiers(reservationDateRangeViewModel.StartDate,
                            reservationDateRangeViewModel.EndDate, material);
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

        public ActionResult CreateReservation(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, NewReservationViewModel newReservationModel)
        {
            var res = user.CreateReservation(reservationDateRangeViewModel.StartDate, reservationDateRangeViewModel.EndDate);
            foreach (var details in newReservationModel.ReservationMaterials.Materials.Select(material => _reservationRepository.GetAvailableIdentifiers(
                res.StartDate,
                res.EndDate,
                material.RequestedAmount,
                _materialRepository.FindById(material.Material.Id))
                .Select(mi => new ReservationDetail(res, mi)).ToList()))
            {
                res.AddAllDetails(details);
            }

            return View("Index");
        }

    }
}