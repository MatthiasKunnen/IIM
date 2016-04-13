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
    public class ReservationController : Controller
    {
        private IUserRepository _userRepository;
        private IReservationRepository _reservationRepository;
        private IMaterialRepository _materialRepository;

        public ReservationController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        // GET: Reservation
        public ActionResult Index()
        {
            var cart = new Cart();
            _materialRepository.FindAll().ForEach(m => cart.Materials.Add(m));

            DateTime dt = DateTime.Now.AddDays(14).StartOfWeek(DayOfWeek.Monday);
            ViewBag.StartDatum = dt.ToString("dd MMMM yyyy");

            return View(cart.Materials.Select(m=>new MaterialViewModel(m)).ToList());
        }
        
    }
   

}