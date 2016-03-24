using System;
using IIM.Models.Domain;
using IIM.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace IIM.Controllers
{
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
            if (!String.IsNullOrEmpty(searchName) )
            {
                return View(_materialRepository
                .FindAll()
                .Where( m => m.Name.Contains(searchName) ||
                             m.Description.Contains(searchName))
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
            }

            return View(_materialRepository
                .FindAll()
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
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