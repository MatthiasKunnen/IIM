using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIM.Controllers;
using IIM.Models;
using IIM.Models.Domain;
using IIM.Tests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IIM.Tests.Domain
{
    [TestClass]
    public class ReservationTest
    {
        private DummyDataContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
        }

        [TestMethod]
        public void DetailsToStringTestShort()
        {
            var res = _context.res2.DetailToString();
            Assert.AreEqual(res, "bal ");
        }

        [TestMethod]
        public void DetailsToStringTestLong()
        {
            var res = _context.res1.DetailToString();
            Assert.AreEqual(res, "bal scrumboard scrumboard hamer hamer ");
        }

        [TestMethod]
        public void ReservationBodyTestShort()
        {
            var exp = $"Beste Pieter Post\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {DateTime.Today.ToShortDateString()}\nTerugbrengen : {DateTime.Today.AddDays(10).ToShortDateString()}\n\nGereserveerde items: \nbal: 1\n\nBedankt voor het gebruikmaken van onze service.";


            var res = _context.res2.ReservationBody;
            Assert.AreEqual(exp,res);
        }

        [TestMethod]
        public void ReservationBodyTestLong()
        {
            var exp = $"Beste Jan Test\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {DateTime.Today.AddDays(11).ToShortDateString()}\nTerugbrengen : {DateTime.Today.AddDays(15).ToShortDateString()}\n\nGereserveerde items: \nbal: 1\nscrumboard: 2\nhamer: 2\n\nBedankt voor het gebruikmaken van onze service.";


            string.Format(
                    "Beste {0} {1}\n\nHierbij een bevestiging van uw reservatie.\nOphalen : {2}\nTerugbrengen : {3}\n\nGereserveerde items: {4}\n\nMet vriendelijke groet\nIIM",
                    "Jan",
                    "Test",
                    DateTime.Today.AddDays(11).ToShortDateString(),
                    DateTime.Today.AddDays(15).ToShortDateString(),
                    "bal scrumboard scrumboard hamer hamer ");
            var res = _context.res1.ReservationBody;
            Assert.AreEqual(exp, res);
        }

        [TestMethod]
        public void IsCompletedYes()
        {
            Assert.AreEqual(_context.res3.IsCompleted(),true);
        }

        [TestMethod]
        public void IsCompletedNo()
        {
            Assert.AreEqual(_context.res1.IsCompleted(), false);
        }

        [TestMethod]
        public void IsCompletedHalf()
        {
            Assert.AreEqual(_context.res2.IsCompleted(), false);
        }

        [TestMethod]
        public void IsEverythingHereYes()
        {
            Assert.AreEqual(_context.res3.IsEverythingHere(),true);
        }

        [TestMethod]
        public void IsEverythingHereNoPending()
        {
            Assert.AreEqual(_context.res2.IsEverythingHere(), false);
        }

        [TestMethod]
        public void IsEverythingHereNo()
        {
            Assert.AreEqual(_context.res1.IsEverythingHere(), false);
        }




    }

    [TestClass]
    public class MaterialIdentifierTest
    {
        private DummyDataContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
        }

        [TestMethod]
        public void IsHereYes()
        {
            var id = _context.Hamer.Identifiers.First();
            Assert.AreEqual(id.IsHere(), true);
        }

        [TestMethod]
        public void IsHereNo()
        {
            var id = _context.Bal.Identifiers.First();
            Assert.AreEqual(id.IsHere(), false);
        }

    }
}
