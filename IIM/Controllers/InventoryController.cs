using System;
using System.Collections.Generic;
using System.Globalization;
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
        private IMaterialRepository _materialRepository;
        public InventoryController(IMaterialRepository repository)
        {
            this._materialRepository = repository;
        }

        // GET
        public ActionResult Index(string searchName)
        {
            var list = _materialRepository
                .FindAll();
            if (!string.IsNullOrEmpty(searchName))
            {
                list = list.Where(m => m.Name.IndexOf(searchName, StringComparison.OrdinalIgnoreCase) >= 0 || m.Description.IndexOf(searchName, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return View(list.ToList()
                .Select(m => new MaterialViewModel(m))
                .OrderBy(mvm => mvm.Name));
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