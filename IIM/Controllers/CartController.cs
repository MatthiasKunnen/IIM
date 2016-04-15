using System.Linq;
using System.Web.Mvc;
using IIM.Models.Domain;
using IIM.ViewModels;
using IIM.Models;

namespace IIM.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMaterialRepository _materialRepository;

        public CartController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        public ActionResult Index(ApplicationUser user)
        {
            return View(user.WishList.Materials.Select(m => new MaterialViewModel(m)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplicationUser user, int id)
        {
            TempData["error"] = "Het item kon niet uit de verlanglijstje verwijderd worden.";
            var materialsInCart = user.WishList.Materials;
            var materialToDelete = materialsInCart.First(m => m.Id == id);
            if (materialsInCart.Remove(materialToDelete))
            {
                _userRepository.SaveChanges();
                TempData["success"] = $"{materialToDelete.Name} werd verwijderd uit de verlanglijstje.";
                TempData.Remove("error");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ApplicationUser user, int id)
        {
            TempData["error"] = "Het materiaal kon niet toegevoegd worden.";
            var material = _materialRepository.FindById(id);
            user.WishList.AddMaterial(material);
            _userRepository.SaveChanges();
            TempData.Remove("error");
            TempData["success"] = $"\"{material.Name}\" werd toegevoegd aan uw verlanglijstje.";
            return RedirectToAction("Index", "Inventory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear(ApplicationUser user)
        {
            TempData["error"] = "Uw verlanglijstje kon niet leeggemaakt worden.";
            _userRepository.ClearWishList(user);
            _userRepository.SaveChanges();
            TempData.Remove("error");
            TempData["success"] = "Uw verlanglijstje werd geleegd.";
            return RedirectToAction("Index", "Cart");
        }
    }
}