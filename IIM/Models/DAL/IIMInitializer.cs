using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using IIM.Models.Domain;

namespace IIM.Models.DAL
{
    public class IIMInitializer : CreateDatabaseIfNotExists<IIMContext>
    {
        /*
        protected override void Seed(IIMContext context)
        {
            try
            {
                var opleiding = new Curricular() { Name = "Opleiding" };
                var targetGroup = new TargetGroup() { Name = "Studenten" };
                var wereldbol = new Material()
                {
                    Id = 1,
                    Name = "Wereldbol",
                    ArticleNr = "WB12",
                    Curriculars = new List<Curricular>() { opleiding },
                    Description = "Wereldbol wordt voor de lessen aardrijkskunde gebruikt",
                    Price = 11.50M,
                    Firm = new Firm() { Name = "Atlas" },
                    TargetGroups = new List<TargetGroup>() { targetGroup }
                };
                var voetbal = new Material()
                {
                    Id = 2,
                    Name = "Voetbal",
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
                var s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.GetValidationResult()}\" has the following validation errors:";
                    s = eve.ValidationErrors.Aggregate(s, (current, ve) => $"{current} - Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                }
                throw new Exception(s);
            }
        }*/
    }
}