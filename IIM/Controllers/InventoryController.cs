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
            AddFilter(filterList, searchCurricular, m => string.Join(" ", m.Curriculars.Select(c => c.Name)));
            AddFilter(filterList, searchFirm, m => m.Firm?.Name);
            AddFilter(filterList, searchTargetGroup, m => string.Join(" ", m.TargetGroups.Select(t => t.Name)));

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
                    throw new InvalidEnumArgumentException(nameof(user.Type), (int)user.Type, typeof(Type));
            }
            filterList.Add(m => m.Identifiers.Any(searchFunc));
            filterList.ForEach(f => list = list.Where(f));

            ViewBag.WishList = user?.WishList;

            return View(list.ToList()
                .Select(m => new MaterialViewModel(m))
                .OrderBy(mvm => mvm.Name));
        }

        private static void AddFilter(ICollection<Func<Material, bool>> filterList, string searchTerm, params Func<Material, string>[] searchedProperty)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                filterList.Add(m => searchedProperty.Any(p => p.Invoke(m)?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0));
            }

        }

        public ActionResult Detail(ApplicationUser user, int id)
        {
            Material material = _materialRepository.FindById(id);
            if (material == null)
                return HttpNotFound();
            ViewBag.WishList = user?.WishList;
            return View(new MaterialViewModel(material));
        }

    }
}