using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIM.Tests.Controllers;
using IIM.Models.Domain;

namespace IIM.Tests.Models.Domain
{
    [TestClass]
    public class CartTest
    {
        private DummyDataContext _context;
        private Cart _cart;
        [TestInitialize()]
        public void Initialize()
        {
            _context = new DummyDataContext();
            _cart = _context.WishList;
        }
        [TestMethod]
        public void MaterialAlreadyInCartTest()
        {
            Assert.IsTrue(_cart.AlreadyInCart(_context.Bal.Id));
            Assert.IsFalse(_cart.AlreadyInCart(_context.Werelbol.Id));
        }

    }
}
