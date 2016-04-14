using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IIM.Models.DAL;
using IIM.Models.Domain;
using IIM.ViewModels;
using IIM.Helpers;

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
        public ActionResult Index()
        {
            return View(_userRepository.GetCurrentUser().WishList.Materials.Select(m => new MaterialViewModel(m)));
        }

        public ActionResult Delete(int id)
        {
            TempData["error"] = "Het item kon niet uit de winkelwagen verwijderd worden.";
            var materialsInCart = _userRepository.GetCurrentUser().WishList.Materials;
            var materialToDelete = materialsInCart.First(m => m.Id == id);
            if (materialsInCart.Remove(materialToDelete))
            {
                _userRepository.SaveChanges();
                TempData["success"] = $"{materialToDelete.Name} werd verwijderd uit de winkelwagen.";
                TempData.Remove("error");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Add(int id)
        {
            TempData["error"] = "Het materiaal kon niet toegevoegd worden.";
            var material = _materialRepository.FindById(id);
            _userRepository.GetCurrentUser().WishList.AddMaterial(material);
            _userRepository.SaveChanges();
            TempData.Remove("error");
            TempData["success"] = $"\"{material.Name}\" werd toegevoegd aan uw winkelwagen.";
            return RedirectToAction("Index", "Inventory");
        }

        public ActionResult Clear()
        {
            TempData["error"] = "Uw winkelwagen kon niet leeggemaakt worden.";
            _userRepository.ClearWishList(_userRepository.GetCurrentUser());
            _userRepository.SaveChanges();
            TempData.Remove("error");
            TempData["success"] = "Uw winkelwagen werd geleegd.";
            return RedirectToAction("Index", "Cart");
        }
    }
}