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
            return View(user.WishList?.Materials.Select(m => new MaterialViewModel(m)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ApplicationUser user, int id)
        {
            var materialToDelete = user.GetMaterialFromCart(id);
            if (user.RemoveMaterialFromCart(materialToDelete))
            {
                _userRepository.SaveChanges();
                TempData["success"] = $"{materialToDelete.Name} werd verwijderd uit uw verlanglijst.";
            }
            else
            {
                TempData["error"] = "Het item kon niet uit uw verlanglijst verwijderd worden.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ApplicationUser user, int id)
        {
            var material = _materialRepository.FindById(id);
            if (user.AddMaterialToCart(material))
            {
                _userRepository.SaveChanges();
                TempData["success"] = $"\"{material.Name}\" werd toegevoegd aan uw verlanglijst.";
            }
            else
            {
                TempData["error"] = "Het materiaal kon niet toegevoegd worden aan uw verlanglijst.";
            }
            return RedirectToAction("Index", "Inventory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear(ApplicationUser user)
        {
            TempData["error"] = "Uw verlanglijst kon niet leeggemaakt worden.";
            user.ClearWishList();
            _userRepository.SaveChanges();
            TempData.Remove("error");
            TempData["success"] = "Uw verlanglijst werd geleegd.";
            return RedirectToAction("Index", "Cart");
        }
    }
}