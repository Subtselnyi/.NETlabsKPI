using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using LogicZaliznitsya.BLL.Infrastructure;
using NovaZaliznitsya.Util;

namespace NovaZaliznitsya
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // внедрение зависимостей
            NinjectModule trainModule = new TrainModule();
            NinjectModule carriageModule = new CarriageModule();
            NinjectModule adminModule = new AdminModule();
            NinjectModule serviceModule = new ServiceModule("UkrZal");
            var kerneltrain = new StandardKernel(trainModule, carriageModule,adminModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kerneltrain));;
        }
    }
}
