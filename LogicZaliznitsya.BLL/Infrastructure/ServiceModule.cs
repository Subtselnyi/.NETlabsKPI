using System;
using System.Collections.Generic;
using System.Text;
using Ninject.Modules;
using DataZaliznitsya.DAL.Interfaces;
using DataZaliznitsya.DAL.Repositories;
 
namespace LogicZaliznitsya.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
