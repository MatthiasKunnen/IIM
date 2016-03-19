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
        private IMaterialRepository materialRepository;
        public StoreController() { }
        public StoreController(IMaterialRepository repository)
        {
            this.materialRepository = repository;
        }
        
        // GET
        public ActionResult Index()
        {
            //IEnumerable<Material> materials = materialRepository.FindAll();
            Curricular opleiding = new Curricular() { Name = "Opleiding" };
            TargetGroup targetGroup = new TargetGroup() { Name = "Studenten" };
            Material wereldbol = new Material()
            {
                Id = 1,
                Name = "Wereldbol",
                Amount = 3,
                ArticleNr = "WB12",
                Curriculars = new List<Curricular>() { opleiding },
                Description = "Wereldbol wordt voor de lessen aardrijkskunde gebruikt",
                Price = 11.50M,
                Firm = new Firm() { Name = "Atlas" },
                TargetGroups = new List<TargetGroup>() { targetGroup }
            };
            Material voetbal = new Material()
            {
                Id = 2,
                Name = "Voetbal",
                Amount = 2,
                ArticleNr = "10BAL41",
                Curriculars = new List<Curricular>() { opleiding },
                Description = "Voetbal wordt gebruikt om de kinderen bezig te houden",
                Price = 6.99M,
                Firm = new Firm() { Name = "Bals&Co" },
                TargetGroups = new List<TargetGroup>() { targetGroup }
            };
            List<Material> materials = new List<Material>();
            materials.Add(wereldbol);
            materials.Add(voetbal);
            IEnumerable<MaterialViewModels> materialViewModels = materials.Select(m => new MaterialViewModels() {
                Name = m.Name,
                Amount = m.Amount,
                Curriculars=m.Curriculars,
                TargetGroups=m.TargetGroups,
                Description=m.Description,
                ArticleNr=m.ArticleNr,
                Firm=m.Firm,
                Price=m.Price
            }).ToList();
            return View(materialViewModels);
        }
    }
}