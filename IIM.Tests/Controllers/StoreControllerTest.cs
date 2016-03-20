using IIM.Controllers;
using IIM.Models.Domain;
using IIM.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IIM.Tests.Controllers
{
    [TestClass]
    class StoreControllerTest
    {
        private StoreController controller;
        private Mock<IMaterialRepository> materialRepository;

        [TestInitialize]
        public void Initialize()
        {
            DummyDataContext context = new DummyDataContext();
            materialRepository = new Mock<IMaterialRepository>();
            materialRepository.Setup(m => m.FindAll()).Returns(context.Materials);
            controller = new StoreController(materialRepository.Object);
        }

        [TestMethod]
        public void IndexReturnsMaterials()
        {
            ViewResult result = controller.Index() as ViewResult;
            IList<MaterialViewModel> materials = result.ViewData.Model as List<MaterialViewModel>;
            Assert.AreEqual(2, materials.Count);
        }
    }
}
