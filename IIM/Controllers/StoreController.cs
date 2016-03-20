using IIM.Models.DAL;
using IIM.Models.Domain;
using IIM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IIM.Controllers
{
    public class StoreController : Controller
    {
        private IMaterialRepository _materialRepository;
        public StoreController() { }
        public StoreController(IMaterialRepository repository)
        {
            this._materialRepository = repository;
        }
        
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}