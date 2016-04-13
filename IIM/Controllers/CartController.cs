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
        private IMaterialRepository _materialRepository;

        public CartController(IUserRepository users, IReservationRepository reservations, IMaterialRepository materials)
        {
            _userRepository = users;
            _reservationRepository = reservations;
            _materialRepository = materials;
        }
        public ActionResult Index()
        {
            return View(Account.GetUser().WishList.Materials
                .Select(m => new MaterialViewModel(m))
                .ToList());
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
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