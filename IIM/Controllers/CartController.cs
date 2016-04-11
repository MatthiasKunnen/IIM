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
        IUserRepository _userRepository;

        public CartController(IUserRepository users)
        {
            _userRepository = users;
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}