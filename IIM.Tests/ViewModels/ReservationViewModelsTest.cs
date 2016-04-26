using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIM.Tests.Controllers;
using IIM.ViewModels.ReservationViewModels;

namespace IIM.Tests.ViewModels.ReservationViewModelsTest
{

    [TestClass]
    public class ReservationDetailViewModelTest
    {
        private DummyDataContext _context;
        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
        }
        [TestMethod]
        public void TestConstructor()
        {
            /*Zelfde error als IndexGetWishlistUser */
            ReservationDetailViewModel rdvm = new ReservationDetailViewModel(_context.Res1_Detail1);
            Assert.AreEqual(new DateTime(2016, 04, 16), rdvm.BroughtBackDate);
            Assert.AreEqual(new DateTime(2016, 04, 21), rdvm.PickUpDate);
            Assert.AreEqual(_context.Werelbol, rdvm.Material);
        }
    }
}
