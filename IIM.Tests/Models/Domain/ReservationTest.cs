using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIM.Tests.Controllers;
using IIM.Models.Domain;
using System.Collections.Generic;

namespace IIM.Tests.Models.Domain
{
    [TestClass]
    public class ReservationTest
    {
        private DummyDataContext _context;
        private Reservation _reservation;
        private ReservationDetail _detail1;
        private ReservationDetail _detail2;
        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
            _reservation = _context.Reservation1;
            
        }
        [TestMethod]
        public void TestReservationConstructor()
        {
            Assert.AreEqual(new DateTime(2016, 04, 16), _reservation.CreationDate);
            Assert.AreEqual(new DateTime(2016, 04, 21), _reservation.StartDate);
            Assert.AreEqual(new DateTime(2016, 04, 25), _reservation.EndDate);
            Assert.AreEqual(_context.User, _reservation.User);
        }

        [TestMethod]
        public void TestAddDetail()
        {
            //AddDetail gebeurt al in de DummyDataContext wanneer de ReservationDetail gedeclareerd worden
            Assert.IsTrue(_reservation.Details.Contains(_context.Detail1));
        }

        [TestMethod]
        public void TestAddAllDetails()
        {
            _reservation.Details.Clear();
            List<ReservationDetail> details = new List<ReservationDetail>() { _context.Detail1, _context.Detail2 };
            _reservation.AddAllDetails(details);
            Assert.AreEqual(_reservation.Details.Count, details.Count);
            Assert.IsTrue(_reservation.Details.Contains(_context.Detail1));
            Assert.IsTrue(_reservation.Details.Contains(_context.Detail2));
        }
    }
}
