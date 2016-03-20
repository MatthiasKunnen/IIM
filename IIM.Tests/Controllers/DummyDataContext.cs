using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IIM.Models.Domain;

namespace IIM.Tests.Controllers
{
    class DummyDataContext
    {
        public IQueryable<Material> Materials {get; set; }
        public IQueryable<TargetGroup> TargetGroups { get; set; }
        public IQueryable<Curricular> Curriculars { get; set; }
        public DummyDataContext()
        {
            Curricular lo = new Curricular() { Name = "Lichaamelijke opvoeding" };
            Curricular analyse = new Curricular() { Name = "Analyse" };
            Curriculars = (new Curricular[] { lo, analyse }).ToList().AsQueryable();
            TargetGroup studentenEersteGraad = new TargetGroup() { Name = "Studenten 1ste graad" };
            TargetGroup studentenTweedeGraad = new TargetGroup() { Name = "Studenten 2de graad" };
            TargetGroups= (new TargetGroup[] {studentenEersteGraad, studentenTweedeGraad}).ToList().AsQueryable();
            //Materiaal die volledig in orde is.
            Material wereldbol = new Material()
            {
                Id = 1,
                Name = "Wereldbol",
                Amount = 3,
                ArticleNr = "WB12",
                Curriculars = Curriculars.ToList(),
                Description = "Wereldbol wordt voor de lessen aardrijkskunde gebruikt",
                Price = 11.50M,
                Firm = new Firm() { Name = "Atlas" },
                TargetGroups = TargetGroups.ToList()
            };
            //Materiaal zonder curriculars
            Material voetbal = new Material()
            {
                Id = 2,
                Name = "Voetbal",
                Amount = 2,
                ArticleNr = "10BAL41",
                Curriculars = null,
                Description = "Voetbal wordt gebruikt om de kinderen bezig te houden",
                Price = 11.90M,
                Firm = new Firm() { Name = "Bals&Co" },
                TargetGroups = TargetGroups.ToList()
            };
            Materials = (new Material[] { wereldbol, voetbal }).ToList().AsQueryable();
            
           
        }
    }
}
