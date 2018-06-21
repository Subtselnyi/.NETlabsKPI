using System;
using System.Collections.Generic;
using System.Text;
using LogicZaliznitsya.BLL.DTO;

namespace LogicZaliznitsya.BLL.Interfaces
{
    public interface IAdminService
    {
        bool GetAdmin(AdministrationDTO admin);
        void Dispose();
    }
}
