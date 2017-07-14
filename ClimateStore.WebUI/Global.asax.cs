using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClimateStore.WebUI.Infrastructure;
using ClimateStore.Domain.Entities;
using ClimateStore.WebUI.Binders;

namespace ClimateStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            //сообщаем MVC Framework, что он может использовать класс CartModelBinder для создания экземпляров объекта Cart
            ModelBinders.Binders.Add(typeof(Cart),new CartModelBinder());
        }
    }
}
