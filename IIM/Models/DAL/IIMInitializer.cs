using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using IIM.Models.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IIM.Models.DAL
{
    public class IIMInitializer : CreateDatabaseIfNotExists<IIMContext>
    {
        
        protected override void Seed(IIMContext context)
        {
           
        }
    }
}