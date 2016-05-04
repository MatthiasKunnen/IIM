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
        private readonly IUserRepository _userRepository;

        public ReservationController(IMaterialRepository materialRepository, IReservationRepository reservationRepository, IUserRepository userRepository)
        {
            _materialRepository = materialRepository;
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
        }


        public ActionResult Index(ApplicationUser user)
        {
            return View(user.Reservations.Where(r =>!r.isCompleted()).OrderBy(r=> r.StartDate).Select(r => new ReservationViewModel(r)));
        }

        public ActionResult Create(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel)
        {
            reservationDateRangeViewModel.SetType(user.Type);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplicationUser user,int id)
        {
            TempData["error"] = "De reservatie kon niet verwijderd worden.";

            var res = _reservationRepository.FindById(id);
            if (res != null)
            {
                user.DeleteReservation(res);
                _userRepository.SaveChanges();
                    TempData["success"] = "De reservatie werd verwijderd.";
                    TempData.Remove("error");
                
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIdentifier(ApplicationUser user,int mat_id, int rid)
        {
            TempData["error"] = "De reservatie kon niet verwijderd worden.";

            var res = _reservationRepository.FindById(rid);
            var mat_iden = res.Details.First(r => r.MaterialIdentifier.Id == mat_id).MaterialIdentifier;
            if (res != null && mat_iden!=null)
            {
                user.DeleteReservationMaterial(res, mat_iden);
                _userRepository.SaveChanges();
                TempData["success"] = "De reservatie werd verwijderd.";
                TempData.Remove("error");

            }
            return RedirectToAction("Index");

        }
    }
}