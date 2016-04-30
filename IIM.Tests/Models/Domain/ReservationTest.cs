using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIM.Tests.Controllers;
using IIM.Models.Domain;
using Moq;
using System.Collections.Generic;
using IIM.Models;

namespace IIM.Tests.Models.Domain
{
    [TestClass]
    public class ReservationTest
    {
        private DummyDataContext _context;
        private Reservation _reservation;
        private ReservationDetail _Res1_Detail1;
        private ReservationDetail _Res1_Detail2;
        private ApplicationUser _spyUser;
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
            Assert.AreEqual(_context.Student, _reservation.User);
        }

        [TestMethod]
        public void TestAddDetail()
        {
            //AddDetail gebeurt al in de DummyDataContext wanneer de ReservationDetail gedeclareerd worden
            Assert.IsTrue(_reservation.Details.Contains(_context.Res1_Detail1));
        }

        [TestMethod]
        public void TestAddAllDetails()
        {
            _reservation.Details.Clear();
            List<ReservationDetail> details = new List<ReservationDetail>() { _context.Res1_Detail1, _context.Res1_Detail2 };
            _reservation.AddAllDetails(details);
            Assert.AreEqual(_reservation.Details.Count, details.Count);
            Assert.IsTrue(_reservation.Details.Contains(_context.Res1_Detail1));
            Assert.IsTrue(_reservation.Details.Contains(_context.Res1_Detail2));
        }

        [TestMethod]
        public void RemoveDetail()
        {
            _reservation.RemoveDetail(_context.Res1_Detail1);
            Assert.IsFalse(_reservation.Details.Contains(_context.Res1_Detail1));
        }
        [TestMethod]
        public void RemoveAllDetails()
        {
            List<ReservationDetail> details = new List<ReservationDetail>() { _context.Res1_Detail1, _context.Res1_Detail2 };
            _reservation.RemoveAllDetails(details);
            Assert.IsFalse(_reservation.Details.Contains(_context.Res1_Detail1));
            Assert.IsFalse(_reservation.Details.Contains(_context.Res1_Detail2));
        }
        
        [TestMethod]
        public void GetOverridableIdentifiersTest()
        {      
            List<ReservationDetail> list = new List<ReservationDetail> { _context.Res1_Detail1 };
            Assert.AreEqual(list.Count, _reservation.GetOverridableIdentifiers(_context.Werelbol).Count);
        }

    }
}
