using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IIM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IIM.Helpers
{
    public class Account
    {
      
        public static ApplicationUser GetUser()
        {
            var user = HttpContext.Current.User;
            return user?.Identity != null && user.Identity.IsAuthenticated ? 
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByName(user.Identity.Name) : 
                null;
        }
    }
}