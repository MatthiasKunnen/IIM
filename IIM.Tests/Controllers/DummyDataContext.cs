using System;
using System.Linq;
using IIM.Models.Domain;
using IIM.Models;
using System.Collections.Generic;
using Type = IIM.Models.Domain.Type;

namespace IIM.Tests.Controllers
{
    class DummyDataContext
    {
        public IQueryable<Material> Materials { get; set; }
        public IQueryable<TargetGroup> TargetGroups { get; set; }
        public IQueryable<Curricular> Curriculars { get; set; }
        public IQueryable<Reservation> Reservations { get; set; }

        public Material Bal { get; set;}
        public Material Scrumboard { get; set;}
        public Material Hamer { get; set; }

        public Firm Firma { get; set; }
        public Firm Firma2 { get; set; }

        public Curricular Lo { get; set; }
        public Curricular Analyse { get; set; }

        public TargetGroup EersteGraad { get; set; }
        public TargetGroup TweedeGraad { get; set; }

        public ApplicationUser User1{ get; set; }
        public ApplicationUser User2 { get; set; }

        public Reservation res1 { get; set; }
        public Reservation res2 { get; set; }
        public Reservation res3 { get; set; }
        public DummyDataContext()
        {
            User1 = new ApplicationUser();
            User1.FirstName = "Jan";User1.LastName = "Test";User1.IsLocal = true;User1.Type = Type.Student;User1.Faculty = "Schoonmeersen";
            User2 = new ApplicationUser();
            User2.FirstName = "Pieter";User2.LastName = "Post";User2.IsLocal = true; User2.Type = Type.Student;User2.Faculty="Test";
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
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(ScrumboardIdentifier2, 3);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(ScrumboardIdentifier2, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(ScrumboardIdentifier2, Scrumboard);


            IList<MaterialIdentifier> scrumboardIdentifiers = (new MaterialIdentifier[] {ScrumboardIdentifier1,ScrumboardIdentifier2 }).ToList();

            typeof(Material).GetProperty("Identifiers").SetValue(Scrumboard, scrumboardIdentifiers);
            /*END MATERIAL ==> SCRUMBOARD*/

            /*MATERIAL ==> Hamer*/
            Hamer = new Material();
            typeof(Material).GetProperty("Id").SetValue(Hamer, 10);
            typeof(Material).GetProperty("Name").SetValue(Hamer, "hamer");
            typeof(Material).GetProperty("TargetGroups").SetValue(Hamer, TargetGroups.ToList());
            typeof(Material).GetProperty("Curriculars").SetValue(Hamer, Curriculars.ToList());
            typeof(Material).GetProperty("Firm").SetValue(Hamer, Firma2);


            MaterialIdentifier HamerIdentifier1 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(HamerIdentifier1, 4);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(HamerIdentifier1, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(HamerIdentifier1, Hamer);

            MaterialIdentifier HamerIdentifier2 = new MaterialIdentifier();
            typeof(MaterialIdentifier).GetProperty("Id").SetValue(HamerIdentifier2, 5);
            typeof(MaterialIdentifier).GetProperty("Visibility").SetValue(HamerIdentifier2, Visibility.Student);
            typeof(MaterialIdentifier).GetProperty("Material").SetValue(HamerIdentifier2, Hamer);


            IList<MaterialIdentifier>hamerIdentifiers = (new MaterialIdentifier[] { HamerIdentifier1, HamerIdentifier2 }).ToList();

            typeof(Material).GetProperty("Identifiers").SetValue(Hamer, hamerIdentifiers);
            /*END MATERIAL ==> Hamer*/


            Materials = (new Material[] { Bal, Scrumboard,Hamer }).ToList().AsQueryable();

            /*Res1*/
            res1 = new Reservation(DateTime.Today.AddDays(10),DateTime.Today.AddDays(11),DateTime.Today.AddDays(15),User1);
            
            ReservationDetail resDet1 = new ReservationDetail(res1,BalIdentifier1);
            ReservationDetail resDet2 = new ReservationDetail(res1,ScrumboardIdentifier1);
            ReservationDetail resDet3 = new ReservationDetail(res1, ScrumboardIdentifier2);
            ReservationDetail resDet4 = new ReservationDetail(res1,HamerIdentifier1);
            ReservationDetail resDet5 = new ReservationDetail(res1, HamerIdentifier2);

            IList<ReservationDetail> res1Details =
                (new ReservationDetail[] {resDet1, resDet2, resDet3, resDet4, resDet5}).ToList();

            typeof(Reservation).GetProperty("Details").SetValue(res1,res1Details);
                /*Res1*/

            /*Res2*/
            res2 = new Reservation(DateTime.Today,DateTime.Today,DateTime.Today.AddDays(10),User2);
            ReservationDetail resDet6 = new ReservationDetail(res2,BalIdentifier1);
            typeof(ReservationDetail).GetProperty("PickUpDate").SetValue(resDet6,DateTime.Today);

            IList<ReservationDetail> res2Details =
               (new ReservationDetail[] { resDet6}).ToList();

            typeof(Reservation).GetProperty("Details").SetValue(res2,res2Details);
            /*Res2*/

            /*Res3*/
            res3 = new Reservation(DateTime.Today, DateTime.Today, DateTime.Today, User2);
            ReservationDetail resDet7 = new ReservationDetail(res3,HamerIdentifier2);
            typeof(ReservationDetail).GetProperty("PickUpDate").SetValue(resDet7,DateTime.Today);
            typeof(ReservationDetail).GetProperty("BroughtBackDate").SetValue(resDet7, DateTime.Today);
            IList<ReservationDetail> res3Details = (new ReservationDetail[] { resDet7 }).ToList();
            typeof(Reservation).GetProperty("Details").SetValue(res3,res3Details);

            /*Res3*/

            IList<ReservationDetail> bal1Res = (new ReservationDetail[] { resDet1,resDet6 }).ToList();
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(BalIdentifier1,bal1Res);
            IList<ReservationDetail> scrum1Res = (new ReservationDetail[] { resDet2 }).ToList();
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(ScrumboardIdentifier1, scrum1Res);
            IList<ReservationDetail> scrum2Res = (new ReservationDetail[] { resDet3 }).ToList();
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(ScrumboardIdentifier2, scrum2Res);
            IList<ReservationDetail> hamer1Res = (new ReservationDetail[] { resDet4 }).ToList();
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(HamerIdentifier1, hamer1Res);
            IList<ReservationDetail> hamer2Res = (new ReservationDetail[] { resDet5,resDet7 }).ToList();
            typeof(MaterialIdentifier).GetProperty("ReservationDetails").SetValue(HamerIdentifier2, hamer2Res);

            Reservations = (new Reservation[] {res1, res2,res3}).ToList().AsQueryable();

        }
    }
}
