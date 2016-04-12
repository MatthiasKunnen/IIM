using IIM.Models.DAL;
using IIM.Models.Domain;
using IIM.ViewModels;
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
        private IMaterialRepository _materialRepository;

        public CartController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        // GET: Cart
        public ActionResult Index()
        {
            User u = new User();
            u.WishList.AddMaterial(_materialRepository.FindById(1));
            u.WishList.AddMaterial(_materialRepository.FindById(2));



            return View(u.WishList.Materials
                .Select(m => new MaterialViewModel(m))
                .ToList());
        }

    }
}