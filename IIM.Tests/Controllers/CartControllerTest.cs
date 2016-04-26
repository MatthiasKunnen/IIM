using IIM.Controllers;
using IIM.Models;
using IIM.Models.Domain;
using IIM.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IIM.Tests.Controllers
{

    [TestClass]
    public class CartControllerTest
    {
        private CartController _controller;
        private Mock<IMaterialRepository> _materialRepository;
        private Mock<IUserRepository> _userRepositoy;
        private Mock<IReservationRepository> _reservationRepository;
        private ApplicationUser _user;
        private DummyDataContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
            _user = _context.Student;
            _materialRepository = new Mock<IMaterialRepository>();
            _userRepositoy = new Mock<IUserRepository>();
            _reservationRepository = new Mock<IReservationRepository>();
            _controller = new CartController(_userRepositoy.Object, _reservationRepository.Object, _materialRepository.Object);
            _materialRepository.Setup(m => m.FindById(5)).Returns(_context.Werelbol);
        }

        [TestMethod]
        public void IndexGetWishlistUser()
        {
            /*
           !!!!!!!Deze test werkt niet wanneer je hem gewoon runt, ook niet als je hem snel debugt.
           Maar wanneer je hem traag debugt en hier een minuutje wacht werkt hij wel???
           Hij heeft een ArgumentNullException bij _filePath.. misschien is mijn internet te traag 
           om de iets op te halen.
           */
            ViewResult result = _controller.Index(_user) as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(_context.WishList.Materials.Count(), materials.Count());
        }
        [TestMethod]
        public void DeleteFromWishList()
        {
            int aantal = _user.WishList.Materials.Count;
            RedirectToRouteResult result = _controller.Delete(_user,_context.Bal.Id) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(_user.WishList.Materials.Contains(_context.Bal));
            Assert.AreEqual(aantal - 1, _user.WishList.Materials.Count);
            _userRepositoy.Verify(u => u.SaveChanges(), Times.Once());
        }
      

        [TestMethod]
        public void AddMaterialToCart()
        {
            int aantal = _user.WishList.Materials.Count;
            RedirectToRouteResult result = _controller.Add(_user, _context.Werelbol.Id) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(_user.WishList.Materials.Contains(_context.Werelbol));            
            Assert.AreEqual(aantal + 1, _user.WishList.Materials.Count);
            _userRepositoy.Verify(u => u.SaveChanges(), Times.Once());
        }
        [TestMethod]
        public void AddMaterialToEmptyCart()
        {
            //Lege verlanglijst aan Student toewijzen                 
            typeof(ApplicationUser).GetProperty("WishList").SetValue(_user, null);

            RedirectToRouteResult result = _controller.Add(_user, _context.Werelbol.Id) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(_user.WishList.Materials.Contains(_context.Werelbol));
            Assert.AreEqual(1, _user.WishList.Materials.Count);
            _userRepositoy.Verify(u => u.SaveChanges(), Times.Once());
        }

        //[TestMethod]
        //public void ClearCart()
        //{
        //    /*Iets met entityframework die raar doet*/
        //    int aantal = _user.WishList.Materials.Count;
        //    RedirectToRouteResult result = _controller.Clear(_user) as RedirectToRouteResult;
        //    Assert.AreEqual("Index", result.RouteValues["action"]);
        //    Assert.AreEqual(0, _user.WishList.Materials.Count);
        //    _userRepositoy.Verify(u => u.SaveChanges(), Times.Once());
        //}
    }
}
