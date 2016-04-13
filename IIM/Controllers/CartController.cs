using IIM.Models;
using IIM.Models.DAL;
using IIM.Models.Domain;
using IIM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
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
<<<<<<< HEAD
        private readonly IMaterialRepository _materialRepository;
=======
        private IMaterialRepository _materialRepository;
        private Cart cart = new Cart();
>>>>>>> 9f451aff4606cd6edb317b5c6be6deb0b7885fe7

        public CartController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        public ActionResult Index()
        {
<<<<<<< HEAD
            return View(Account.GetUser().WishList.Materials.Select(m=>new MaterialViewModel(m)));
=======
             
            _materialRepository.FindAll().ForEach(m => cart.Materials.Add(m));

            //Dit is de minimale startdatum dat een reservatie kan gemaakt worden 
            //dus minimum een week tussen en dan de eerste maandag
            DateTime dt = DateTime.Now.AddDays(14).StartOfWeek(DayOfWeek.Monday);
            ViewBag.StartDatum = dt.ToString("dd MMMM yyyy");

            return View(cart.Materials
                .Select(m => new MaterialViewModel(m))
                .ToList());
>>>>>>> 9f451aff4606cd6edb317b5c6be6deb0b7885fe7
        }

        public ActionResult RemoveFromCart(int id, Cart cart)
        {
            //werkt maar doordat hij terugkeert naar Index wordt het weer gevuld natuurlijk + user moet nog upgedate worden
            Material material = _materialRepository.FindById(id);
            if (material == null)
                return HttpNotFound();
            cart.RemoveMaterial(material);
            TempData["message"] = String.Format("{0} werd verwijderd", material.Name);
            return RedirectToAction("Index");
            }

    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
<<<<<<< HEAD
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
=======

            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
            
>>>>>>> 9f451aff4606cd6edb317b5c6be6deb0b7885fe7
        }

    }
}