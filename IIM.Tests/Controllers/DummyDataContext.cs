using System.Linq;
using IIM.Models.Domain;
using IIM.Models;
using System.Collections.Generic;

namespace IIM.Tests.Controllers
{
    class DummyDataContext
    {
        public IQueryable<Material> Materials { get; set; }
        public IQueryable<TargetGroup> TargetGroups { get; set; }
        public IQueryable<Curricular> Curriculars { get; set; }
        public Material Bal { get; set;}
        public Material Scrumboard { get; set;}
        public Firm Firma { get; set; }
        public Firm Firma2 { get; set; }

        public Curricular Lo { get; set; }
        public Curricular Analyse { get; set; }
        public TargetGroup EersteGraad { get; set; }
        public TargetGroup TweedeGraad { get; set; }
        public ApplicationUser User { get; set; }

        public DummyDataContext()
        {
            User = new ApplicationUser();
            User.FirstName = "Jan";User.LastName = "Test";User.IsLocal = true;User.Type = Type.Student;User.Faculty = "Schoonmeersen";
            Lo = new Curricular();
            typeof(Curricular).GetProperty("Id").SetValue(Lo, 1);
            typeof(Curricular).GetProperty("Name").SetValue(Lo, "Lichamelijke opvoeding");
            Analyse = new Curricular();
            typeof(Curricular).GetProperty("Id").SetValue(Analyse, 2);
            typeof(Curricular).GetProperty("Name").SetValue(Analyse, "Analyse");
            Curriculars = (new Curricular[] { Lo, Analyse }).ToList().AsQueryable();

            EersteGraad = new TargetGroup();
            typeof(TargetGroup).GetProperty("Id").SetValue(EersteGraad, 1);
            typeof(TargetGroup).GetProperty("Name").SetValue(EersteGraad, "eersteGraad");
            TweedeGraad = new TargetGroup();
            typeof(TargetGroup).GetProperty("Id").SetValue(TweedeGraad, 2);
            typeof(TargetGroup).GetProperty("Name").SetValue(TweedeGraad, "tweedeGraad");
            TargetGroups = (new TargetGroup[] { EersteGraad, TweedeGraad }).ToList().AsQueryable();

            Firma = new Firm();
            typeof(Firm).GetProperty("Id").SetValue(Firma, 1);
            typeof(Firm).GetProperty("Name").SetValue(Firma, "firma");
            Firma2 = new Firm();
            typeof(Firm).GetProperty("Id").SetValue(Firma2, 2);
            typeof(Firm).GetProperty("Name").SetValue(Firma2, "firma2");


            /*MATERIAL ==> BAL*/
            Bal = new Material();
            typeof(Material).GetProperty("Id").SetValue(Bal, 2);
            typeof(Material).GetProperty("Name").SetValue(Bal, "bal");
            typeof(Material).GetProperty("Firm").SetValue(Bal, Firma);
            typeof(Material).GetProperty("TargetGroups").SetValue(Bal, TargetGroups.ToList());
            typeof(Material).GetProperty("Curriculars").SetValue(Bal, Curriculars.ToList());

            MaterialIdentifier BalIdentifier1 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(BalIdentifier1, 1);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(BalIdentifier1, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(BalIdentifier1, Bal);

            IList<MaterialIdentifier> balIdentifiers = (new MaterialIdentifier[] { BalIdentifier1 }).ToList();

            typeof(Material).GetProperty("Identifiers").SetValue(Bal, balIdentifiers);
            /*END MATERIAL ==> BAL*/


            /*MATERIAL ==> SCRUMBOARD*/
            Scrumboard = new Material();
            typeof(Material).GetProperty("Id").SetValue(Scrumboard, 10);
            typeof(Material).GetProperty("Name").SetValue(Scrumboard, "scrumboard");
            typeof(Material).GetProperty("TargetGroups").SetValue(Scrumboard, TargetGroups.ToList());
            typeof(Material).GetProperty("Curriculars").SetValue(Scrumboard, Curriculars.ToList());
            typeof(Material).GetProperty("Firm").SetValue(Scrumboard, Firma2);


            MaterialIdentifier ScrumboardIdentifier1 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(ScrumboardIdentifier1, 2);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(ScrumboardIdentifier1, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(ScrumboardIdentifier1, Scrumboard);

            MaterialIdentifier ScrumboardIdentifier2 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(ScrumboardIdentifier2, 1);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(ScrumboardIdentifier2, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(ScrumboardIdentifier2, Scrumboard);


            IList<MaterialIdentifier> scrumboardIdentifiers = (new MaterialIdentifier[] {ScrumboardIdentifier1,ScrumboardIdentifier2 }).ToList();

            typeof(Material).GetProperty("Identifiers").SetValue(Scrumboard, scrumboardIdentifiers);
            /*END MATERIAL ==> SCRUMBOARD*/
            Materials = (new Material[] { Bal, Scrumboard }).ToList().AsQueryable();

        }
    }
}
