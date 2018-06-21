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
    public class TrainService : ITrainService
    {
        IUnitOfWork Database { get; set; }

        public TrainService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Delete(int id)
        {            
                TrainDetail train = Database.TrainDetails.Get(id);
                if (train == null)
                    throw new ValidationException("Поезд не найден", "");
                Database.TrainDetails.Delete(train.Id);
                Database.Save();
            
        }

        public TrainDetailDTO GetTrain(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id", "");
            var train = Database.TrainDetails.Get((int)id);
            if (train == null)
                throw new ValidationException("Поезд не найден", "");
            return new TrainDetailDTO { Train_Name = train.Train_Name, Id = train.Id, Train_Desc = train.Train_Desc, From_City = train.From_City, To_City = train.To_City, ArrivalDate = train.ArrivalDate, DepartureDate = train.DepartureDate };
        }
        

        public IEnumerable<TrainDetailDTO> GetAllTrains()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TrainDetail, TrainDetailDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TrainDetail>, List<TrainDetailDTO>>(Database.TrainDetails.GetAll());
        }


        public IEnumerable<TrainDetailDTO> GetTrainsFT(string From, string To)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TrainDetail, TrainDetailDTO>()).CreateMapper();
            if (From == null)
                return mapper.Map<IEnumerable<TrainDetail>, List<TrainDetailDTO>>(Database.TrainDetails.Find(d => { return (d.To_City == To); } ));
            else if (To == null)
                return mapper.Map<IEnumerable<TrainDetail>, List<TrainDetailDTO>>(Database.TrainDetails.Find(d => { return (d.From_City == From); }));
            else if (From == null && To == null)
                return mapper.Map<IEnumerable<TrainDetail>, List<TrainDetailDTO>>(Database.TrainDetails.GetAll());
            else
                return mapper.Map<IEnumerable<TrainDetail>, List<TrainDetailDTO>>(Database.TrainDetails.Find(d => { return (d.From_City == From & d.To_City == To); }));

        }


        public void Update(TrainDetailDTO train)
        {
            
            TrainDetail Train = Database.TrainDetails.Get(train.Id);
            if (Train == null)
                throw new ValidationException("Поезд не найден", "");
            Train.Id = train.Id;
            Train.ArrivalDate = train.ArrivalDate;
            Train.DepartureDate = train.DepartureDate;
            Train.From_City = train.From_City;
            Train.To_City = train.To_City;
            Train.Train_Name = train.Train_Name;
            Train.Train_Desc = train.Train_Desc;

            Database.TrainDetails.Update(Train);
            Database.Save();
        }

        public void Create(TrainDetailDTO train)
        {
            TrainDetail Train = new TrainDetail();
            Train.ArrivalDate = train.ArrivalDate;
            Train.DepartureDate = train.DepartureDate;
            Train.From_City = train.From_City;
            Train.To_City = train.To_City;
            Train.Train_Name = train.Train_Name;
            Train.Train_Desc = train.Train_Desc;

            Database.TrainDetails.Create(Train);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
