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
    public class HomeController : Controller
    {
        ITrainService trainService;
        public HomeController(ITrainService serv)
        {
            trainService = serv;
        }
        public ActionResult Index()
        {
            IEnumerable<TrainDetailDTO> trainDtos = trainService.GetAllTrains();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TrainDetailDTO, TrainViewModel>()).CreateMapper();
            var trains = mapper.Map<IEnumerable<TrainDetailDTO>, List<TrainViewModel>>(trainDtos);
            ViewBag.IsAdmin = IsAdmin;
            return View(trains);
        }

        [HttpPost]
        public ActionResult TrainsSearch(string From = null, string To = null)
        {
            IEnumerable<TrainDetailDTO> trainDtos = trainService.GetTrainsFT(From, To);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TrainDetailDTO, TrainViewModel>()).CreateMapper();
            var trains = mapper.Map<IEnumerable<TrainDetailDTO>, List<TrainViewModel>>(trainDtos);
            ViewBag.IsAdmin = IsAdmin;
            return View("Index",trains);
        }

        public ActionResult Edit(int? id)
        {
            if (IsAdmin)
            {
                try
                {
                    TrainDetailDTO train = trainService.GetTrain(id);
                    var Train = new TrainViewModel { Train_Name = train.Train_Name, Id = train.Id, Train_Desc = train.Train_Desc, From_City = train.From_City, To_City = train.To_City, ArrivalDate = train.ArrivalDate, DepartureDate = train.DepartureDate };

                    return View(Train);
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(TrainViewModel train)
        {
            if (IsAdmin)
            {
                try
                {
                    var trainDto = new TrainDetailDTO { Train_Name = train.Train_Name, Id = train.Id, Train_Desc = train.Train_Desc, From_City = train.From_City, To_City = train.To_City, ArrivalDate = train.ArrivalDate, DepartureDate = train.DepartureDate };
                    trainService.Update(trainDto);
                    return Content("<h2>Поезд успешно обновлен</h2> <a href='/'> На главную </a>");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                return View(train);
            }
            return RedirectToAction("Index");
        }



        public ActionResult Insert()
        {
            if (IsAdmin)
            {
                var Train = new TrainViewModel() ;

                return View(Train);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult Insert(TrainViewModel train)
        {
            if (IsAdmin)
            {
                try
                {
                    var trainDto = new TrainDetailDTO { Train_Name = train.Train_Name, Train_Desc = train.Train_Desc, From_City = train.From_City, To_City = train.To_City, ArrivalDate = train.ArrivalDate, DepartureDate = train.DepartureDate };
                    trainService.Create(trainDto);
                    return Content("<h2>Поезд успешно добавлен</h2> <a href='/'> На главную </a>");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                return View(train);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Delete(int id)
        {
            if (IsAdmin)
            {
                try
                {
                    TrainDetailDTO train = trainService.GetTrain(id);
                    trainService.Delete(train.Id);

                    return Content("<h2>Поезд успешно удален</h2> <a href='/'> На главную </a>");
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);
                }
            }
            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
           return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public bool IsAdmin { get { return Session["IsAdmin"] != null && (bool)Session["IsAdmin"]; } }

        protected override void Dispose(bool disposing)
        {
            trainService.Dispose();
            base.Dispose(disposing);
        }
    }
}
