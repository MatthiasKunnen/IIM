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
        public ApplicationUser Student { get; set; }
        public ApplicationUser Staff { get; set; }

        public Cart WishList { get; set; }
        public Cart WishListStaff { get; set; }
        public Material Werelbol { get; set; }
        public Reservation Reservation1 { get; set; }
        public Reservation Reservation2 { get; set; }
        public List<Reservation> Reservations { get; set; }
        public ReservationDetail Res1_Detail1 { get; set; }
        public ReservationDetail Res1_Detail2 { get; set; }
        public ReservationDetail Res2_Detail1 { get; set; }
        public ReservationDetail Res2_Detail2 { get; set; }
        public DummyDataContext()
        {
            #region Student
            Student = new ApplicationUser();
            Student.FirstName = "Jan"; Student.LastName = "Test"; Student.IsLocal = true; Student.Type = IIM.Models.Domain.Type.Student; Student.Faculty = "Schoonmeersen";
            #endregion
            #region Staff
            Staff = new ApplicationUser();
            Staff.FirstName = "Staff"; Staff.LastName = "Naam"; Staff.IsLocal = true; Staff.Type = IIM.Models.Domain.Type.Staff; Staff.Faculty = "Schoonmeersen";
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

            #region WishList opvullen Student
            /*Student zijn WishList vullen*/
            IList<Material> materials = (new Material[] { Bal, Scrumboard }).ToList();
            WishList = new Cart();
            typeof(Cart).GetProperty("Id").SetValue(WishList, 1);
            typeof(Cart).GetProperty("Materials").SetValue(WishList, materials);
            typeof(ApplicationUser).GetProperty("WishList").SetValue(Student, WishList);
            #endregion
            #region WishList opvullen Staff
            /*Staff zijn WishList vullen*/
            IList<Material> materialsStaff = (new Material[] { Bal, Scrumboard }).ToList();
            WishListStaff = new Cart();
            typeof(Cart).GetProperty("Id").SetValue(WishListStaff, 1);
            typeof(Cart).GetProperty("Materials").SetValue(WishListStaff, materialsStaff);
            typeof(ApplicationUser).GetProperty("WishList").SetValue(Staff, WishListStaff);
            #endregion

            #region Reservation1
            Reservation1 = new Reservation(new DateTime(2016, 04, 16), new DateTime(2016, 04, 21), new DateTime(2016, 04, 25), Student);
            typeof(Reservation).GetProperty("Id").SetValue(Reservation1, 1);
            Res1_Detail1 = new ReservationDetail(Reservation1, WerelbolIdentifier1);
            typeof(ReservationDetail).GetProperty("BroughtBackDate").SetValue(Res1_Detail1, new DateTime(2016, 04, 21));
            typeof(ReservationDetail).GetProperty("PickUpDate").SetValue(Res1_Detail1, new DateTime(2016, 04, 16));
            Res1_Detail2 = new ReservationDetail(Reservation1, BalIdentifier1); //Nog niet teruggebracht
            typeof(ReservationDetail).GetProperty("PickUpDate").SetValue(Res1_Detail2, new DateTime(2016, 04, 21));
            typeof(Reservation).GetProperty("Details").SetValue(Reservation1, (new ReservationDetail[] { Res1_Detail1, Res1_Detail2 }).ToList());


            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(WerelbolIdentifier1, (new ReservationDetail[] { Res1_Detail1 }).ToList());
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(BalIdentifier1, (new ReservationDetail[] { Res1_Detail2 }).ToList());

            #endregion


            #region Reservation2
            Reservation2 = new Reservation(new DateTime(2016, 04, 26), new DateTime(2016, 04, 30), new DateTime(2016, 05, 03), Student);
            typeof(Reservation).GetProperty("Id").SetValue(Reservation2, 1);
            Res2_Detail1 = new ReservationDetail(Reservation2, WerelbolIdentifier2);
            typeof(ReservationDetail).GetProperty("BroughtBackDate").SetValue(Res2_Detail1, new DateTime(2016, 05, 03));
            typeof(ReservationDetail).GetProperty("PickUpDate").SetValue(Res2_Detail1, new DateTime(2016, 04, 30));
            Res2_Detail2 = new ReservationDetail(Reservation1, BalIdentifier1); //Nog niet teruggebracht
            typeof(ReservationDetail).GetProperty("PickUpDate").SetValue(Res2_Detail2, new DateTime(2016, 04, 30));
            typeof(Reservation).GetProperty("Details").SetValue(Reservation2, (new ReservationDetail[] { Res2_Detail1, Res2_Detail2 }).ToList());

            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(WerelbolIdentifier2, (new ReservationDetail[] { Res2_Detail1 }).ToList());
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(BalIdentifier1, (new ReservationDetail[] { Res2_Detail2 }).ToList());

            #endregion

            Reservations = (new Reservation[] { Reservation1, Reservation2 }).ToList();
            typeof(ApplicationUser).GetProperty("Reservations").SetValue(Student, Reservations);

        }
    }
}
