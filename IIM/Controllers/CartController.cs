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
            
            return View(cart.Materials
                .Select(m => new MaterialViewModel(m))
                .ToList());
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Add(int id)
        {
            throw new NotImplementedException();
        }
    }
}