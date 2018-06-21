using System;
using System.Collections.Generic;
using System.Text;
using LogicZaliznitsya.BLL.DTO;

namespace LogicZaliznitsya.BLL.Interfaces
{
    public interface ITrainService
    {
        IEnumerable<TrainDetailDTO> GetAllTrains();
        IEnumerable<TrainDetailDTO> GetTrainsFT(string From, string To);
        TrainDetailDTO GetTrain(int? id);
        void Update(TrainDetailDTO train);
        void Create(TrainDetailDTO train);
        void Delete(int id);
        void Dispose();
    }
}
