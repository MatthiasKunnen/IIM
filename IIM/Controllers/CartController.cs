using IIM.Models;
using IIM.Models.DAL;
using IIM.Models.Domain;
using IIM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace IIM.Controllers
{
    public class CartController : Controller
    {
        private IUserRepository _userRepository;
        private IReservationRepository _reservationRepository;
        private IMaterialRepository _materialRepository;

        public CartController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        public ActionResult Index()
        {
            var cart = new Cart();
            _materialRepository.FindAll().ForEach(m => cart.Materials.Add(m));

            //Dit is de minimale startdatum dat een reservatie kan gemaakt worden 
            //dus minimum een week tussen en dan de eerste maandag
            DateTime dt = DateTime.Now.AddDays(14).StartOfWeek(DayOfWeek.Monday);
            ViewBag.StartDatum = dt.ToString("dd MMMM yyyy");

            return View(cart.Materials
                .Select(m => new MaterialViewModel(m))
                .ToList());
        }

        public ActionResult RemoveFromCart(int id, Cart cart)
        {
            //werkt maar doordat hij terugkeert naar Index wordt het weer gevuld natuurlijk
            Material material = _materialRepository.FindById(id);
            cart.RemoveMaterial(material);
            TempData["message"] = material.Name + " werd verwijderd uit je mandje";
            return RedirectToAction("Index");
        }

    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
    }
}