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
    public class CarriageController : Controller
    {
        ICarriageService carriageService;

        public CarriageController(ICarriageService serv)
        {
            carriageService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<CarriageDetailDTO> carriageDtos = carriageService.GetAllCarriages();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarriageDetailDTO, CarriageViewModel>()).CreateMapper();
            var carriages = mapper.Map<IEnumerable<CarriageDetailDTO>, List<CarriageViewModel>>(carriageDtos);
            ViewBag.IsAdmin = IsAdmin;
            return View(carriages);
        }

        public ActionResult ViewCarriages(int id, string t_name)
        {
            try
            {
                IEnumerable<CarriageDetailDTO> carriageDtos = carriageService.GetCarriagesByTrain(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarriageDetailDTO, CarriageViewModel>()).CreateMapper();
                var carriages = mapper.Map<IEnumerable<CarriageDetailDTO>, List<CarriageViewModel>>(carriageDtos);
                Session["T_Name"] = t_name;
                Session["T_Id"] = id;
                ViewBag.IsAdmin = IsAdmin;
                return View(carriages);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                CarriageDetailDTO carriage = carriageService.GetCarriage(id);
                IEnumerable<BookingTableDTO> bookings = carriageService.GetAvailableSeats(id);               
                List<int> seats = new List<int>();
                foreach (var item in bookings)
                {
                    seats.Add(item.Seat_Num);
                }
                var carr = new CarriageViewModel { Id = carriage.Id , Carriage_Desc = carriage.Carriage_Desc, Amount = carriage.Amount};
                ViewBag.Train_Name = T_Name;
                ViewBag.Bookings = seats;
                ViewBag.Train_Id = carriage.Train_Id;
                return View(carr);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        
        public ActionResult Book(int? id)
        {
            try
            {
                CarriageDetailDTO carriage = carriageService.GetCarriage(id);
                var book = new BookingViewModel { Carriage_Id = carriage.Id };
                IEnumerable<BookingTableDTO> bookings = carriageService.GetAvailableSeats((int)id);
                List<int> seats = new List<int>();
                foreach (var item in bookings)
                {
                    seats.Add(item.Seat_Num);
                }
                ViewBag.Bookings = seats;
                ViewBag.Amount = carriage.Amount;
                ViewBag.Train_Id = T_Id;
                ViewBag.Train_Name = T_Name;

                return View(book);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        
        [HttpPost]
        public ActionResult Book(BookingViewModel booking)
        {
            try
            {
                var bookingTableDto = new BookingTableDTO { Carriage_Id = booking.Carriage_Id, NAME = booking.NAME, EMAIL = booking.EMAIL, Description = booking.Description, Seat_Num = booking.Seat_Num };
                carriageService.BookPlace(bookingTableDto);
                return Content("<h2>Ваш заказ успешно оформлен</h2> <a href='/'> На главную </a>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(booking);
        }

        public ActionResult Edit(int? id)
        {
            if (IsAdmin)
            {
                try
                {
                    CarriageDetailDTO carriage = carriageService.GetCarriage(id);
                    var Carriage = new CarriageViewModel { Id = carriage.Id, Train_Id = carriage.Train_Id, Amount = carriage.Amount, Carriage_Desc = carriage.Carriage_Desc };
                    ViewBag.Train_Name = T_Name;
                    return View(Carriage);
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(CarriageViewModel carriage)
        {
            if (IsAdmin)
            {
                try
                {
                    var carriageDto = new CarriageDetailDTO {Id = carriage.Id, Train_Id = carriage.Train_Id, Amount = carriage.Amount, Carriage_Desc = carriage.Carriage_Desc };
                    carriageService.Update(carriageDto);
                    return Content("<h2>Поезд успешно обновлен</h2> <a href='/'> На главную </a>");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                return View(carriage);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Insert()
        {
            if (IsAdmin)
            {
                var Carriage = new CarriageViewModel();
               
                ViewBag.train_Name = T_Name;
                ViewBag.train_Id = T_Id;
                return View(Carriage);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult Insert(CarriageViewModel carriage)
        {
            if (IsAdmin)
            {
                try
                {
                    var Carriage = new CarriageDetailDTO { Train_Id = carriage.Train_Id, Amount = carriage.Amount, Carriage_Desc = carriage.Carriage_Desc };

                    carriageService.Create(Carriage);
                    return Content("<h2>Вагон успешно добавлен</h2> <a href='/'> На главную </a>");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                return View(carriage);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Delete(int id)
        {
            if (IsAdmin)
            {
                try
                {
                    CarriageDetailDTO carriage = carriageService.GetCarriage(id);
                    carriageService.Delete(carriage.Id);

                    return Content("<h2>вагон успешно удален</h2> <a href='/'> На главную </a>");
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);
                }
            }
            return RedirectToAction("Index");

        }


        public bool IsAdmin { get { return Session["IsAdmin"] != null && (bool)Session["IsAdmin"]; } }

        public string T_Name { get { return Session["T_Name"].ToString(); }  }

        public  int T_Id { get { return (int)Session["T_Id"]; } }

        protected override void Dispose(bool disposing)
        {
            carriageService.Dispose();
            base.Dispose(disposing);
        }
    }
}