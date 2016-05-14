using IIM.ViewModels.ReservationViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using IIM.Helpers;
using IIM.Models;
using IIM.Models.Domain;
using IIM.App_Start;
using Type = IIM.Models.Domain.Type;

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
            var reservations =
                user.Reservations.Where(r => !r.IsCompleted())
                    .OrderBy(r => r.StartDate)
                    .Select(r => new ReservationViewModel(r));
            return Request.IsAjaxRequest() ? (ActionResult) PartialView("Reservations", reservations) : View(reservations);
        }

        public ActionResult Create(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel)
        {
            reservationDateRangeViewModel.UserType = user.Type;
            CheckDateRangeModel(reservationDateRangeViewModel, reservationDateRangeViewModel.StartDate, reservationDateRangeViewModel.EndDate, false);
            Func<Material, int> func = material =>
                material.GetAvailableIdentifiers(reservationDateRangeViewModel.StartDate,
                    reservationDateRangeViewModel.EndDate).Count();
            var materials = user.WishList.Materials.Select(m => new ReservationDetailSelectionViewModel(m, func.Invoke(m), 0));
            var materialsPicker = new NewReservationMaterialsViewModel(materials, false);
            var wrapper = new NewReservationViewModel(reservationDateRangeViewModel, materialsPicker);
            return View(wrapper);
        }

        [HttpPost]
        public ActionResult ChangeReservationRange(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, DateTime startDate, DateTime? endDate)
        {
            if (ModelState.IsValid)
            {
                reservationDateRangeViewModel.UserType = user.Type;
                reservationDateRangeViewModel.StartDate = startDate;
                if (endDate != null) reservationDateRangeViewModel.EndDate = endDate.Value;
                CheckDateRangeModel(reservationDateRangeViewModel, reservationDateRangeViewModel.StartDate, reservationDateRangeViewModel.EndDate);
            }
            return RedirectToAction("Create", reservationDateRangeViewModel);
        }

        public async Task<ActionResult> CreateReservation(ApplicationUser user, ReservationDateRangeViewModel reservationDateRangeViewModel, NewReservationMaterialsViewModel reservationMaterialsViewModel)
        {
            var requestedMaterials = reservationMaterialsViewModel.Materials
                .ToDictionary(detail => _materialRepository.FindById(detail.Material.Id), detail => detail.RequestedAmount);
            user.CreateReservation(reservationDateRangeViewModel.StartDate, reservationDateRangeViewModel.EndDate, requestedMaterials);
            _reservationRepository.SaveChanges();
            await MailService.SendMailAsync(user.Reservations.Last().ReservationBody, "Reservering IIM", user.Email);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveMaterial(ApplicationUser user, int? id)
        {
            if (!id.HasValue)
            {
                TempData["error"] = "Het item kon niet verwijderd worden.";
            }
            else
            {
                var materialToDelete = _materialRepository.FindById(id.Value);
                if (user.RemoveMaterialFromCart(materialToDelete))
                {
                    _userRepository.SaveChanges();
                    TempData["success"] = $"{materialToDelete.Name} werd verwijderd.";
                }
                else
                {
                    TempData["error"] = "Het item kon niet verwijderd worden.";
                }
                if (user.WishList.Materials.Count <= 0)
                {
                    TempData["success"] = "Het laatste material uit uw verlanglijstje werd verwijderd, u dient een nieuwe materialen toe te voegen om verder te gaan";
                    return RedirectToAction("Index", "Inventory");
                }
            }
            return RedirectToAction("Create");

        }

        private static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            var result = start.AddDays(1);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }

        private static DateTime? GetDefaultEndDateTime(Type userType)
        {
            var rangeRestriction = AppSettings.GetEndDateRangeRestriction(userType);
            return rangeRestriction == null ? (DateTime?)null : ModernizeTime(rangeRestriction.DefaultTime, rangeRestriction.DefaultDay);
        }

        private static DateTime? GetDefaultStartDateTime(Type userType)
        {
            var rangeRestriction = AppSettings.GetStartDateRangeRestriction(userType);
            return rangeRestriction == null ? (DateTime?)null : ModernizeTime(rangeRestriction.DefaultTime, rangeRestriction.DefaultDay);
        }

        private static DateTime ModernizeTime(DateTime date, DayOfWeek dayOfWeek)
        {
            return GetNextWeekday(DateTime.Today.AddMinutes(date.TimeOfDay.TotalMinutes), dayOfWeek);
        }

        public void CheckDateRangeModel(ReservationDateRangeViewModel reservationDateRangeViewModel, DateTime? start, DateTime? end, bool displayError = true)
        {
            if (start == null || !(AppSettings.GetStartDateRangeRestriction(reservationDateRangeViewModel.UserType)?.IsDateValid(start.Value) ?? true))
            {
                if (displayError)
                    reservationDateRangeViewModel.StartDateError = "Deze datum is niet geschikt als begindatum van uw reservatie.";
                var defaultStartDateTime = GetDefaultStartDateTime(reservationDateRangeViewModel.UserType);
                if (defaultStartDateTime != null)
                    reservationDateRangeViewModel.StartDate = (DateTime)defaultStartDateTime;
            }
            else
            {
                reservationDateRangeViewModel.StartDate = start.Value;
                reservationDateRangeViewModel.StartDateError = null;
            }

            if (end == null || !(AppSettings.GetEndDateRangeRestriction(reservationDateRangeViewModel.UserType)?.IsDateValid(end.Value) ?? true))
            {
                if (displayError)
                    reservationDateRangeViewModel.EndDateError = "Deze datum is niet geschikt als einddatum van uw reservatie.";
                var defaultEndDateTime = GetDefaultEndDateTime(reservationDateRangeViewModel.UserType);
                if (defaultEndDateTime != null)
                    reservationDateRangeViewModel.EndDate = (DateTime)defaultEndDateTime;
            }
            else
            {
                reservationDateRangeViewModel.EndDate = end.Value;
                reservationDateRangeViewModel.EndDateError = null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplicationUser user, int id)
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
        public ActionResult DeleteIdentifier(ApplicationUser user, int mat_id, int rid)
        {
            TempData["error"] = "De reservatie kon niet verwijderd worden.";

            var res = _reservationRepository.FindById(rid);
            var mat_iden = res.Details.First(r => r.MaterialIdentifier.Id == mat_id).MaterialIdentifier;
            if (res != null && mat_iden != null)
            {
                user.DeleteReservationMaterial(res, mat_iden);
                _userRepository.SaveChanges();
                TempData["success"] = "Het Materiaal werd verwijderd.";
                TempData.Remove("error");

            }
            return RedirectToAction("Index");

        }

    }
}