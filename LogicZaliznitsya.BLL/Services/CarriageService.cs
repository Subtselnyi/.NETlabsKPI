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
using System.Net.Mail;

namespace LogicZaliznitsya.BLL.Services
{
    public class CarriageService : ICarriageService
    {
        IUnitOfWork Database { get; set; }

        public CarriageService(IUnitOfWork uow)
        {
            Database = uow;
        }        

        public void BookPlace(BookingTableDTO bookt)
        {
            CarriageDetail carriage = Database.CarriageDetails.Get(bookt.Carriage_Id);
            // валидация
            if (carriage == null)
                throw new ValidationException("Вагон не найден", "");

            BookingTable bt = new BookingTable
            {
                CarriageDetail = carriage,
                Carriage_Id = bookt.Carriage_Id,
                Date = DateTime.Now,
                NAME = bookt.NAME,
                EMAIL = bookt.EMAIL,
                Description = bookt.Description,
                Seat_Num = bookt.Seat_Num
            };
           
            Database.BookingTables.Create(bt);
            Database.Save();
            /*
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(bookt.EMAIL);
                mailMessage.From = new MailAddress("info@NovaZaliznitsya.com");
                mailMessage.Subject = "Place booked";
                mailMessage.Body = "Hello. You have booked a ticket via NovaZaliznitsya service. Your place "+ bookt.Seat_Num;
                SmtpClient smtpClient = new SmtpClient("smtp.isp.com");
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }*/
        }

        public CarriageDetailDTO GetCarriage(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id", "");
            var carriage = Database.CarriageDetails.Get((int)id);
            if (carriage == null)
                throw new ValidationException("Вагон не найден", "");
            return new CarriageDetailDTO { Train_Id = carriage.Train_Id, Id = carriage.Id, Carriage_Desc = carriage.Carriage_Desc, Amount = carriage.Amount };
        }

        public void Update(CarriageDetailDTO carriage)
        {
            CarriageDetail carr = Database.CarriageDetails.Get(carriage.Id);
            if (carr == null)
                throw new ValidationException("Вагон не найден", "");
            carr.Train_Id = carriage.Train_Id;
            carr.Carriage_Desc = carriage.Carriage_Desc;
            carr.Amount = carriage.Amount;

            Database.CarriageDetails.Update(carr);
            Database.Save();
        }

        public void Create(CarriageDetailDTO carriage)
        {
            CarriageDetail carr = new CarriageDetail();
            carr.Train_Id = carriage.Train_Id;
            carr.Carriage_Desc = carriage.Carriage_Desc;
            carr.Amount = carriage.Amount;

            Database.CarriageDetails.Create(carr);
            Database.Save();
        }

        public void Delete(int id)
        {
            if (IsAdmin)
            {
                CarriageDetail carriage = Database.CarriageDetails.Get(id);
                if (carriage == null)
                    throw new ValidationException("Вагон не найден", "");
                IEnumerable<BookingTable> bookings = (Database.BookingTables.Find(d => d.Carriage_Id == carriage.Id));
                if (bookings != null && bookings.GetEnumerator().MoveNext()) 
                {
                    throw new ValidationException("В вагоне уже забронированы места", "");
                }
                Database.CarriageDetails.Delete(carriage.Id);
                Database.Save();
            }
        }

        public void DeleteBooking(int id)
        {
            if (IsAdmin)
            {
                BookingTable bt = Database.BookingTables.Get(id);
                if (bt == null)
                    throw new ValidationException("Бронь не найдена", "");
                Database.BookingTables.Delete(bt.Id);
                Database.Save();
            }
        }

        public CarriageDetailDTO Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingTableDTO> GetAvailableSeats(int carr_id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingTable, BookingTableDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<BookingTable>, List<BookingTableDTO>>(Database.BookingTables.Find(d => d.Carriage_Id == carr_id));
        }

        public IEnumerable<CarriageDetailDTO> GetAllCarriages()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarriageDetail, CarriageDetailDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<CarriageDetail>, List<CarriageDetailDTO>>(Database.CarriageDetails.GetAll());
        }

        public IEnumerable<CarriageDetailDTO> GetCarriagesByTrain(int train_id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CarriageDetail, CarriageDetailDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<CarriageDetail>, List<CarriageDetailDTO>>(Database.CarriageDetails.Find(d => d.Train_Id == train_id));
        }

        public bool IsAdmin
        {
            get { return true; /*return Session["IsAdmin"] != null && (bool)Session["IsAdmin"];*/ }
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
