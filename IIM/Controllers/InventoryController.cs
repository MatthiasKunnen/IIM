using System;
using System.Collections.Generic;
using System.ComponentModel;
using IIM.Models.Domain;
using IIM.ViewModels;
using System.Linq;
using System.Web.Mvc;
using IIM.Models;
using Type = IIM.Models.Domain.Type;

namespace IIM.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ICurricularRepository _curricularRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly ITargetGroupRepository _targetGroupRepository;
        private readonly IReservationRepository _reservationRepository;
        public InventoryController(ICurricularRepository curricularRepository, IMaterialRepository materialRepository, ITargetGroupRepository targetGroupRepository, IReservationRepository reservationRepository)
        {
            _curricularRepository = curricularRepository;
            _materialRepository = materialRepository;
            _targetGroupRepository = targetGroupRepository;
            _reservationRepository = reservationRepository;
        }

        public ActionResult Index(ApplicationUser user, string searchName, string searchCurricular, string searchTargetGroup)
        {
            IEnumerable<Material> list = _materialRepository.FindAll();
            var filterList = new List<Func<Material, bool>>();
            Func<MaterialIdentifier, bool> searchFunc;

            switch (user?.Type ?? Type.Student)
            {
                case Type.Staff:
                    searchFunc = identifier => identifier.Visibility != Visibility.Administrator;
                    break;
                case Type.Student:
                    searchFunc = identifier => identifier.Visibility == Visibility.Student;
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(user.Type), user == null ? -1 : (int)user.Type, typeof(Type));
            }

            AddFilter(filterList, searchName, m => m.Name, m => m.Description);
            AddFilter(filterList, searchCurricular, m => string.Join(" ", m.Curriculars.Select(c => c.Name)));
            AddFilter(filterList, searchTargetGroup, m => string.Join(" ", m.TargetGroups.Select(t => t.Name)));

            filterList.Add(m => m.Identifiers.Any(searchFunc));
            filterList.ForEach(f => list = list.Where(f));

            ViewBag.WishList = user?.WishList;
            var materialViewModels = list.ToList().Select(m => new MaterialViewModel(m)).OrderBy(mvm => mvm.Name);

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("MaterialOverview", materialViewModels)
                : View(new InventoryViewModel(
                    _curricularRepository.FindAll().OrderBy(c => c.Name).ToList().Select(c => new SearchableItemModel(c.Id, c.Name)),
                    new SearchableItemModel(0, searchCurricular),
                    materialViewModels,
                    _targetGroupRepository.FindAll().OrderBy(t => t.Name).ToList().Select(t => new SearchableItemModel(t.Id, t.Name)),
                    new SearchableItemModel(0, searchTargetGroup)));
        }

        private static void AddFilter(ICollection<Func<Material, bool>> filterList, string searchTerm, params Func<Material, string>[] searchedProperty)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filterList.Add(m => searchedProperty.Any(p => p.Invoke(m)?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0));
            }
        }

        public ActionResult Detail(ApplicationUser user, int? id)
        {
            if (!id.HasValue)
            {
                return Request.IsAjaxRequest() ? null : RedirectToAction("Index");
            }
            var material = _materialRepository.FindById(id.Value);
            if (material == null)
                return HttpNotFound();

            ViewBag.WishList = user?.WishList;
            
            ViewBag.IsStudent = user?.Type == Type.Student;

            var periods = new List<MaterialReservationDetailsView>();
            var hulpLijst = new List<Reservation>();
            material.Identifiers.ToList().ForEach(i => {
                foreach (var reservatieDetail in i.ReservationDetails.Where(reservatieDetail => hulpLijst.All(r => r.Id != reservatieDetail.Reservation.Id))
                    .Where(reservatieDetail => reservatieDetail.Reservation.StartDate >= DateTime.Now))
                {
                    hulpLijst.Add(reservatieDetail.Reservation);
                    periods.Add(new MaterialReservationDetailsView(reservatieDetail,
                        reservatieDetail.Reservation.Details.Count(d => d.MaterialIdentifier.Material == material),
                        _reservationRepository));
                }
            });

            ViewBag.ShowReturnButton = !Request.IsAjaxRequest();
            var mvm = new MaterialViewModel(material, periods);
            return Request.IsAjaxRequest() ? (ActionResult) PartialView(mvm) : View(mvm);
        }
    }
}