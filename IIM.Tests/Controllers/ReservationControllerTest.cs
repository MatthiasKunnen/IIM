using IIM.Controllers;
using IIM.Models;
using IIM.Models.Domain;
using IIM.ViewModels;
using IIM.ViewModels.ReservationViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IIM.Tests.Controllers
{
    [TestClass]
    public class ReservationControllerTest
    {
        private ReservationController _controller;
        private Mock<IMaterialRepository> _materialRepository;
        private Mock<IReservationRepository> _reservationRepository;
        private ApplicationUser _user;

        private DummyDataContext _context;
        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
            _user = _context.Student;
            _materialRepository = new Mock<IMaterialRepository>();
            _reservationRepository = new Mock<IReservationRepository>();
            _materialRepository.Setup(m => m.FindAll()).Returns(_context.Materials);
            _materialRepository.Setup(m => m.FindById(2)).Returns(_context.Bal);
            _controller = new ReservationController(_materialRepository.Object, _reservationRepository.Object);
        }

        [TestMethod]
        public void IndexReturnsAllReservations()
        {
            /*Wanneer je debugt werkt de test wel.. Iets raars met de _filePath*/
            ViewResult result = _controller.Index(_user) as ViewResult;
            var reservations = result.ViewData.Model as IEnumerable<ReservationViewModel>;
            Assert.AreEqual(_context.Student.Reservations.Count(), reservations.ToList().Count);
        }

        [TestMethod]
        public void CreateReturnsNewReservationViewModel()
        {
            ReservationDateRangeViewModel rdrv = new ReservationDateRangeViewModel(new System.DateTime(2016, 06, 26), new System.DateTime(2016, 07, 03), Type.Student);
            ViewResult result = _controller.Create(_user,rdrv) as ViewResult;
            var newReservation = result.ViewData.Model as NewReservationViewModel;
            Assert.AreEqual(_user.WishList.Materials.Count, newReservation.ReservationMaterials.Materials.Count());
        }


        //[TestMethod]
        //public void IndexReturnsSearchResultsOnCurricularAndName()
        //{
        //    ViewResult result = _controller.Index(_user, "bal", "Lichamelijke opvoeding", "", "") as ViewResult;
        //    var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
        //    Assert.AreEqual(1, materials.ToList().Count);
        //}

        //[TestMethod]
        //public void IndexReturnsSearchResultsOnFirm()
        //{
        //    ViewResult result = _controller.Index(_user, "", "", "firma2", "") as ViewResult;
        //    var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
        //    Assert.AreEqual(1, materials.ToList().Count);
        //}
        //[TestMethod]
        //public void IndexReturnsSearchResultsOnFirmAndTargetgroup()
        //{
        //    ViewResult result = _controller.Index(_user, "", "", "firma2", "tweedeGraad") as ViewResult;
        //    var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
        //    Assert.AreEqual(1, materials.ToList().Count);
        //}


        //[TestMethod]
        //public void DetailReturnsSelectedMaterial()
        //{
        //    ViewResult result = _controller.Detail(_user, 2) as ViewResult;
        //    var m = (MaterialViewModel)result.ViewData.Model;
        //    Assert.AreEqual(2, m.Id);
        //}

    }
}