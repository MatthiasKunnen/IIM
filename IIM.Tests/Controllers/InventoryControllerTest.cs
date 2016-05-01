using IIM.Controllers;
using IIM.Models;
using IIM.Models.Domain;
using IIM.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System;

namespace IIM.Tests.Controllers
{
    [TestClass]
    public class InventoryControllerTest
    {
        private InventoryController _controller;
        private Mock<IMaterialRepository> _materialRepository;
        private Mock<ICurricularRepository> _curricularRepository;
        private Mock<ITargetGroupRepository> _targetGroupRepository;
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
            _curricularRepository = new Mock<ICurricularRepository>();
            _targetGroupRepository = new Mock<ITargetGroupRepository>();
            _materialRepository.Setup(m => m.FindAll()).Returns(_context.Materials);
            _materialRepository.Setup(m => m.FindById(2)).Returns(_context.Bal);
            _controller = new InventoryController(_curricularRepository.Object,_materialRepository.Object, _targetGroupRepository.Object);    
        }

        [TestMethod]
        public void IndexReturnsAllMaterials()
        {
            this.MakeAjaxRequest(false);
            /*Wanneer je debugt lukt het wel.. Nog al zo'n testen gehad met dezelfde fout namelijk iets met de _filePath*/
            ViewResult result = _controller.Index(_user, "", "", "") as ViewResult;
            var inventoryViewModel = result.ViewData.Model as InventoryViewModel;
            Assert.AreEqual(_context.Materials.Count(), inventoryViewModel.MaterialViewModels.ToList().Count);
        }
       
        [TestMethod]
        public void IndexReturnsSearchResultsOnName()
        {
            this.MakeAjaxRequest(true);
            var result = _controller.Index(_user, "bal", "", "") as PartialViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual("MaterialOverview", result.ViewName);
            Assert.AreEqual(1,  materials.ToList().Count);
        }

        [TestMethod]
        public void IndexReturnsSearchResultsOnCurricular()
        {
            this.MakeAjaxRequest(true);
            var result = _controller.Index(_user, "","Analyse","") as PartialViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual("MaterialOverview", result.ViewName);
            Assert.AreEqual(2, materials.ToList().Count);
        }
        [TestMethod]
        public void IndexReturnsSearchResultsOnCurricularAndName()
        {
            this.MakeAjaxRequest(true);
            var result = _controller.Index(_user, "bal", "Lichamelijke opvoeding", "") as PartialViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual("MaterialOverview", result.ViewName);
            Assert.AreEqual(1, materials.ToList().Count);
        }

        [TestMethod]
        public void IndexReturnsSearchResultsOnFirmAndTargetgroup()
        {
            this.MakeAjaxRequest(true);
            var result = _controller.Index(_user, "", "", "tweedeGraad") as PartialViewResult;
            var materials = result.ViewData.Model as IEnumerable<MaterialViewModel>;
            Assert.AreEqual("MaterialOverview", result.ViewName);
            Assert.AreEqual(2, materials.ToList().Count);
        }


        [TestMethod]
        public void DetailReturnsSelectedMaterial()
        {
            ViewResult result = _controller.Detail(_user, 2) as ViewResult;
            var m = (MaterialViewModel)result.ViewData.Model;
            Assert.AreEqual(2, m.Id);
        }
        

        public void MakeAjaxRequest(bool ajaxRequestCreation)
        {
            if (ajaxRequestCreation)
            {
                // First create request with X-Requested-With header set
                Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
                httpRequest.SetupGet(x => x.Headers).Returns(
                    new WebHeaderCollection() {
            {"X-Requested-With", "XMLHttpRequest"}
                    }
                );

                // Then create contextBase using above request
                Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
                httpContext.SetupGet(x => x.Request).Returns(httpRequest.Object);

                // Set controllerContext
                _controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), _controller);
            }
            else
            {
                Mock<HttpRequestBase> request = new Mock<HttpRequestBase>();
                Mock<HttpResponseBase> response = new Mock<HttpResponseBase>();
                Mock<HttpContextBase> context = new Mock<HttpContextBase>();

                context.Setup(c => c.Request).Returns(request.Object);
                context.Setup(c => c.Response).Returns(response.Object);
                _controller.ControllerContext = new ControllerContext(
        context.Object, new RouteData(), _controller);
            }
    }

}
}