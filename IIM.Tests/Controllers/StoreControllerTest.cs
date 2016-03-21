using IIM.Controllers;
using IIM.Models.Domain;
using IIM.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IIM.Tests.Controllers
{
    [TestClass]
    class StoreControllerTest
    {
        private InventoryController _controller;
        private Mock<IMaterialRepository> _materialRepository;

        [TestInitialize]
        public void Initialize()
        {
            DummyDataContext context = new DummyDataContext();
            _materialRepository = new Mock<IMaterialRepository>();
            _materialRepository.Setup(m => m.FindAll()).Returns(context.Materials);
            _controller = new InventoryController(_materialRepository.Object);
        }

        [TestMethod]
        public void IndexReturnsMaterials()
        {
            ViewResult result = _controller.Index() as ViewResult;
            IList<MaterialViewModel> materials = result.ViewData.Model as List<MaterialViewModel>;
            Assert.AreEqual(2, materials.Count);
        }
    }
}
