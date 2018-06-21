using System;
using System.Collections.Generic;
using System.Text;
using LogicZaliznitsya.BLL.DTO;
using DataZaliznitsya.DAL.Entities;
//using LogicZaliznitsya.BLL.BusinessModels;
using DataZaliznitsya.DAL.Interfaces;
using LogicZaliznitsya.BLL.Infrastructure;
using LogicZaliznitsya.BLL.Interfaces;
using System.Web;
using AutoMapper;

namespace LogicZaliznitsya.BLL.Services
{
    public class AdminService : IAdminService
    {
        IUnitOfWork Database { get; set; }

        public AdminService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public bool GetAdmin(AdministrationDTO administrator)
        {
            if (string.IsNullOrWhiteSpace(administrator.Name))
                throw new ValidationException("Не установлено name", "");
            if (string.IsNullOrWhiteSpace(administrator.Password))
                throw new ValidationException("Не установлено password", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Administration, AdministrationDTO>()).CreateMapper();
            var admin = mapper.Map<IEnumerable<Administration>, List<AdministrationDTO>>(Database.Administartions.Find(d => d.Name == administrator.Name));            
            if (admin == null)
                throw new ValidationException("Администратор не найден", "");
            return ((administrator.Password.Trim()).Equals(admin[0].Password));
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
