using System.Linq;
using IIM.Models.Domain;
using IIM.Models;
using System.Collections.Generic;
using System;

namespace IIM.Tests.Controllers
{
    class DummyDataContext
    {
        public IQueryable<Material> Materials { get; set; }
        public IQueryable<TargetGroup> TargetGroups { get; set; }
        public IQueryable<Curricular> Curriculars { get; set; }
        public Material Bal { get; set; }
        public Material Scrumboard { get; set; }
        public Firm Firma { get; set; }
        public Firm Firma2 { get; set; }

        public Curricular Lo { get; set; }
        public Curricular Analyse { get; set; }
        public TargetGroup EersteGraad { get; set; }
        public TargetGroup TweedeGraad { get; set; }
        public ApplicationUser User { get; set; }

        public Cart WishList { get; set; }
        public Material Werelbol { get; set; }
        public Reservation Reservation1 { get; set; }
        public ReservationDetail Detail1 { get; set; }
        public ReservationDetail Detail2 { get; set; }

        public DummyDataContext()
        {
            #region User
            User = new ApplicationUser();
            User.FirstName = "Jan"; User.LastName = "Test"; User.IsLocal = true; User.Type = IIM.Models.Domain.Type.Student; User.Faculty = "Schoonmeersen";
            #endregion

            #region Curriculars
            Lo = new Curricular();
            typeof(Curricular).GetProperty("Id").SetValue(Lo, 1);
            typeof(Curricular).GetProperty("Name").SetValue(Lo, "Lichamelijke opvoeding");
            Analyse = new Curricular();
            typeof(Curricular).GetProperty("Id").SetValue(Analyse, 2);
            typeof(Curricular).GetProperty("Name").SetValue(Analyse, "Analyse");
            Curriculars = (new Curricular[] { Lo, Analyse }).ToList().AsQueryable();
            #endregion

            #region Targetgroups
            EersteGraad = new TargetGroup();
            typeof(TargetGroup).GetProperty("Id").SetValue(EersteGraad, 1);
            typeof(TargetGroup).GetProperty("Name").SetValue(EersteGraad, "eersteGraad");
            TweedeGraad = new TargetGroup();
            typeof(TargetGroup).GetProperty("Id").SetValue(TweedeGraad, 2);
            typeof(TargetGroup).GetProperty("Name").SetValue(TweedeGraad, "tweedeGraad");

            TargetGroups = (new TargetGroup[] { EersteGraad, TweedeGraad }).ToList().AsQueryable();
            #endregion

            #region Firma's
            Firma = new Firm();
            typeof(Firm).GetProperty("Id").SetValue(Firma, 1);
            typeof(Firm).GetProperty("Name").SetValue(Firma, "firma");
            Firma2 = new Firm();
            typeof(Firm).GetProperty("Id").SetValue(Firma2, 2);
            typeof(Firm).GetProperty("Name").SetValue(Firma2, "firma2");
            #endregion

            #region Material: Bal
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
            #endregion

            #region Material: Scrumboard
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

            IList<MaterialIdentifier> scrumboardIdentifiers = (new MaterialIdentifier[] { ScrumboardIdentifier1, ScrumboardIdentifier2 }).ToList();

            typeof(Material).GetProperty("Identifiers").SetValue(Scrumboard, scrumboardIdentifiers);
            #endregion

            #region Material: Wereldbol
            Werelbol = new Material();
            typeof(Material).GetProperty("Id").SetValue(Werelbol, 5);
            typeof(Material).GetProperty("ArticleNr").SetValue(Werelbol, "W01");
            typeof(Material).GetProperty("Description").SetValue(Werelbol, "Dit is de beschrijving van een wereldbol");
            typeof(Material).GetProperty("Price").SetValue(Werelbol, 9.99M);
            typeof(Material).GetProperty("Name").SetValue(Werelbol, "Wereldbol");
            typeof(Material).GetProperty("TargetGroups").SetValue(Werelbol, TargetGroups.ToList());
            typeof(Material).GetProperty("Curriculars").SetValue(Werelbol, Curriculars.ToList());
            typeof(Material).GetProperty("Firm").SetValue(Werelbol, Firma);


            MaterialIdentifier WerelbolIdentifier1 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(WerelbolIdentifier1, 1);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(WerelbolIdentifier1, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Place").SetValue(WerelbolIdentifier1, "B1.012");
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(WerelbolIdentifier1, Werelbol);

            MaterialIdentifier WerelbolIdentifier2 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(WerelbolIdentifier2, 2);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(WerelbolIdentifier2, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(WerelbolIdentifier2, Werelbol);

            IList<MaterialIdentifier> wereldbolIdentifiers = (new MaterialIdentifier[] { WerelbolIdentifier1, WerelbolIdentifier2 }).ToList();

            typeof(Material).GetProperty("Identifiers").SetValue(Werelbol, wereldbolIdentifiers);
            #endregion

            /*Lijst van materialen*/
            Materials = (new Material[] { Bal, Scrumboard }).ToList().AsQueryable();

            #region WishList opvullen
            /*User zijn WishList vullen*/
            IList<Material> materials = (new Material[] { Bal, Scrumboard }).ToList();
            WishList = new Cart();
            typeof(Cart).GetProperty("Id").SetValue(WishList, 1);
            typeof(Cart).GetProperty("Materials").SetValue(WishList, materials);
            typeof(ApplicationUser).GetProperty("WishList").SetValue(User, WishList);
            #endregion

            Reservation1 = new Reservation(new DateTime(2016, 04, 16), new DateTime(2016, 04, 21), new DateTime(2016, 04, 25), User);
            typeof(Reservation).GetProperty("Id").SetValue(Reservation1, 1);

            Detail1 = new ReservationDetail(Reservation1, WerelbolIdentifier1);
            Detail2 = new ReservationDetail(Reservation1, BalIdentifier1);

        }
    }
}
