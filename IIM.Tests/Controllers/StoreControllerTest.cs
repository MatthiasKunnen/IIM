using IIM.Controllers;
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
    public class StoreControllerTest
    {
        private InventoryController _controller;
        private Mock<IMaterialRepository> _materialRepository;
        public IQueryable<Material> Materials { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Material m1 = new Material();
            typeof(Material).GetProperty("Id").SetValue(m1, 2);
            typeof(Material).GetProperty("Name").SetValue(m1, "test");
            Material m2 = new Material();
            typeof(Material).GetProperty("Id").SetValue(m2, 10);
            typeof(Material).GetProperty("Name").SetValue(m2, "testMaterial");

            Materials = (new Material[] { m1, m2 }).ToList().AsQueryable();

           // DummyDataContext context = new DummyDataContext();
            _materialRepository = new Mock<IMaterialRepository>();
            _materialRepository.Setup(m => m.FindAll()).Returns(Materials);
           
            _materialRepository.Setup(m => m.FindById(2)).Returns(m1);
            _controller = new InventoryController(_materialRepository.Object);
        }

        [TestMethod]
        public void IndexReturnsAllMaterials()
        {
            ViewResult result = _controller.Index("") as ViewResult;
            IList<MaterialViewModel> materials = result.ViewData.Model as List<MaterialViewModel>;
            Assert.AreEqual(2, materials.Count);
        }
        [TestMethod]
        public void IndexReturnsSearchResults()
        {
            ViewResult result = _controller.Index("test") as ViewResult;
            IList<MaterialViewModel> materials = result.ViewData.Model as List<MaterialViewModel>;
            Assert.AreEqual(2, materials.Count);
            foreach(var m in materials)
            {
                Assert.IsTrue(m.Name.Contains("test"));
            }
        }

        [TestMethod]
        public void DetailReturnsSelectedMaterial()
        {
            ViewResult result = _controller.Detail(2) as ViewResult;
            var m = (MaterialViewModel) result.ViewData.Model;
           
            Assert.AreEqual(2, m.Id);
        }
       
    }
}
