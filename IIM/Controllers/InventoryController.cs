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
        private IMaterialRepository _materialRepository;
        public InventoryController(IMaterialRepository repository)
        {
            this._materialRepository = repository;
        }

        // GET
        public ActionResult Index(string searchName)
        {
            IEnumerable<MaterialViewModel> list = _materialRepository
                .FindAll()
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .OrderBy(mvm => mvm.Name);
            if (!string.IsNullOrEmpty(searchName))
            {
                list = list.Where(m=>m.Name.Contains(searchName) || m.Description.Contains(searchName));
            }
            return View(list.ToList());
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