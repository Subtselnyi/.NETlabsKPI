using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using LogicZaliznitsya.BLL.Services;
using LogicZaliznitsya.BLL.Interfaces;

namespace NovaZaliznitsya.Util
{
    public class CarriageModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICarriageService>().To<CarriageService>();
        }
    }
}


