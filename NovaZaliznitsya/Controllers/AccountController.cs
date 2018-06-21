using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicZaliznitsya.BLL.Interfaces;
using LogicZaliznitsya.BLL.DTO;
using NovaZaliznitsya.Models;
using AutoMapper;
using LogicZaliznitsya.BLL.Infrastructure;

namespace NovaZaliznitsya.Controllers
{
    public class AccountController : Controller 
    {
        IAdminService adminService;
        public AccountController(IAdminService serv)
        {
            adminService = serv;
        }

        public ActionResult Login(AdministrationDTO admin)
        {
            if (string.IsNullOrWhiteSpace(admin.Password))
            {
                return View();
            }
            if (string.IsNullOrWhiteSpace(admin.Name))
            {
                return View();
            }

            bool IsAdmin = adminService.GetAdmin(admin);
            Session["IsAdmin"] = IsAdmin;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["IsAdmin"] = null;
            return RedirectToAction("Index", "Home");
        }
        
    }
}