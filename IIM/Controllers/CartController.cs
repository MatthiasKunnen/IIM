using IIM.Models.DAL;
using IIM.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IIM.Controllers
{
    public class CartController : Controller
    {
        private IUserRepository _userRepository;
        private IReservationRepository _reservationRepository;

        public CartController(IUserRepository users, IReservationRepository reservations)
        {
            _userRepository = users;
            _reservationRepository = reservations;
        }
        // GET: Cart
        public ActionResult Index()
        {
           


            return View();
        }

    }
}