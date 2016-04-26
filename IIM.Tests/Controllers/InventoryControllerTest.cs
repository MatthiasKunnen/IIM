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
    public class InventoryControllerTest
    {
        private InventoryController _controller;
        private Mock<IMaterialRepository> _materialRepository;
        private Mock<IUserRepository> _userRepositoy;
        private ApplicationUser _user;
        private ApplicationUser _userStaff;

        private DummyDataContext _context;
        [TestInitialize]
        public void Initialize()
        {
            _context = new DummyDataContext();
            _user = _context.Student;
            _userStaff = _context.Staff;
            _materialRepository = new Mock<IMaterialRepository>();
            _userRepositoy = new Mock<IUserRepository>();
            _materialRepository.Setup(m => m.FindAll()).Returns(_context.Materials);
            _materialRepository.Setup(m => m.FindById(2)).Returns(_context.Bal);
            _controller = new InventoryController(_materialRepository.Object, _userRepositoy.Object);
        }

        [TestMethod]
        public void IndexReturnsAllMaterials()
        {
            /*Wanneer je debugt lukt het wel.. Nog al zo'n testen gehad met dezelfde fout namelijk iets met de _filePath*/
            ViewResult result = _controller.Index(_user, "", "", "", "") as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(_context.Materials.Count(), materials.ToList().Count);
        }
       
        [TestMethod]
        public void IndexReturnsSearchResultsOnName()
        {
            ViewResult result = _controller.Index(_user, "bal", "", "", "") as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(1,  materials.ToList().Count);
        }

        [TestMethod]
        public void IndexReturnsSearchResultsOnCurricular()
        {
            ViewResult result = _controller.Index(_user, "","Analyse","","") as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(2, materials.ToList().Count);
        }
        [TestMethod]
        public void IndexReturnsSearchResultsOnCurricularAndName()
        {
            ViewResult result = _controller.Index(_user, "bal", "Lichamelijke opvoeding", "", "") as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(1, materials.ToList().Count);
        }
       
        [TestMethod]
        public void IndexReturnsSearchResultsOnFirm()
        {
            ViewResult result = _controller.Index(_user, "", "", "firma2", "") as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(1, materials.ToList().Count);
        }
        [TestMethod]
        public void IndexReturnsSearchResultsOnFirmAndTargetgroup()
        {
            ViewResult result = _controller.Index(_user, "", "", "firma2", "tweedeGraad") as ViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual(1, materials.ToList().Count);
        }


        [TestMethod]
        public void DetailReturnsSelectedMaterial()
        {
            ViewResult result = _controller.Detail(_user, 2) as ViewResult;
            var m = (MaterialViewModel)result.ViewData.Model;
            Assert.AreEqual(2, m.Id);
        }
        

    }
}