using IIM.Models.Domain;
using IIM.ViewModels;
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
        public ActionResult Index()
        {
            return View(_materialRepository
                .FindAll()
                .OrderBy(m => m.Name)
                .ToList()
                .Select(m => new MaterialViewModel(m))
                .ToList());
        }
    }
}