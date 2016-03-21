using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using IIM.Models.Domain;

namespace IIM.Models.DAL
{
    public class IIMInitializer : DropCreateDatabaseAlways<IIMContext>
    {
        protected override void Seed(IIMContext context)
        {
            try
            {
                Curricular opleiding = new Curricular() { Name = "Opleiding" };
                TargetGroup targetGroup = new TargetGroup() { Name = "Studenten" };
                Material wereldbol = new Material() {
                    Id=1,
                    Name = "Wereldbol",
                    Amount = 3,
                    ArticleNr = "WB12",
                    Curriculars=new List<Curricular>() { opleiding },
                    Description="Wereldbol wordt voor de lessen aardrijkskunde gebruikt",
                    Price=11.50M,
                    Firm= new Firm() { Name="Atlas"},
                    TargetGroups= new List<TargetGroup>() { targetGroup}
                };
                Material voetbal = new Material()
                {
                    Id=2,
                    Name = "Voetbal",
                    Amount = 2,
                    ArticleNr = "10BAL41",
                    Curriculars = new List<Curricular>() { opleiding },
                    Description = "Voetbal wordt gebruikt om de kinderen bezig te houden",
                    Price = 6.99M,
                    Firm = new Firm() { Name = "Bals&Co" },
                    TargetGroups = new List<TargetGroup>() { targetGroup }
                };
                context.Materials.Add(wereldbol);
                context.Materials.Add(voetbal);

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }

        }
        
    }
}