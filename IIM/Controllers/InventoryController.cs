using System;
using System.Collections.Generic;
using IIM.Models.Domain;
using IIM.ViewModels;
using System.Linq;
using System.Web.Mvc;
using IIM.Models;

namespace IIM.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly IUserRepository _userRepository;

        private IMaterialRepository _materialRepository;
        public InventoryController(IMaterialRepository repository, IUserRepository users)
        {
            _userRepository = users;
            this._materialRepository = repository;
        }

        // GET
        public ActionResult Index(ApplicationUser user, string searchName, string searchCurricular, string searchFirm, string searchTargetGroup)
        {
            IEnumerable<Material> list = _materialRepository
                .FindAll();
            var filterList = new List<Func<Material, bool>>();
            AddFilter(filterList, searchName, m => m.Name, m => m.Description);
            AddFilter(filterList, searchCurricular, m => m.Curriculars.Select(c => c.Name).Aggregate((c1, c2) => $"{c1} {c2}"));
            AddFilter(filterList, searchFirm, m => m.Firm.Name);
            AddFilter(filterList, searchTargetGroup, m => m.TargetGroups.Select(c => c.Name).Aggregate((c1, c2) => $"{c1} {c2}"));

            filterList.ForEach(f => list = list.Where(f));

            Cart wishList = user.WishList;
            ViewBag.WishList = wishList;

            return View(list.ToList()
                .Select(m => new MaterialViewModel(m))
                .OrderBy(mvm => mvm.Name));
        }

        private static void AddFilter(ICollection<Func<Material, bool>> filterList, string searchTerm, params Func<Material, string>[] searchedProperty)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filterList.Add(m => searchedProperty.Any(p => p.Invoke(m).IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0));
            }

        }

        public ActionResult Detail(int id)
        {
            Material material = _materialRepository.FindById(id);
            if (material == null)
                return HttpNotFound();
            return View(new MaterialViewModel(material));
        }
    }
}