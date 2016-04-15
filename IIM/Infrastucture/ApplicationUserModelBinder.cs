using System.Web.Mvc;
using IIM.Models.Domain;

namespace IIM.Infrastucture
{
    public class ApplicationUserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return !controllerContext.HttpContext.User.Identity.IsAuthenticated ?
                null :
                ((IUserRepository)DependencyResolver.Current.GetService(typeof(IUserRepository)))
                    .FindByUserName(controllerContext.HttpContext.User.Identity.Name);
        }
    }
}