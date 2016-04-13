using IIM.Models.DAL;
using IIM.Models.Domain;
using IIM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIM.Helpers;
using WebGrease.Css.Extensions;

namespace IIM.Controllers
{
    public class CartController : Controller
    {
        private IUserRepository _userRepository;
        private IReservationRepository _reservationRepository;
        private readonly IMaterialRepository _materialRepository;

        public CartController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        public ActionResult Index()
        {
            return View(Account.GetUser().WishList.Materials.Select(m=>new MaterialViewModel(m)));
        }

        public ActionResult Delete(int id)
        {
            var materialsInCart = Account.GetUser().WishList.Materials;
            var materialToDelete = materialsInCart.First(m => m.Id == id);
            if (materialToDelete != null && materialsInCart.Remove(materialToDelete))
            {
                TempData["success"] = $"{materialToDelete.Name} werd verwijderd uit de winkelwagen.";
            }
            else
            {
                TempData["error"] = $"Het item kon niet uit de winkelwagen verwijderd worden.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(int id)
        { 
            var user = Account.GetUser();
            user.WishList.AddMaterial(_materialRepository.FindById(id));
            return RedirectToAction("Index", "Inventory");
        }
    }
}