using System;
using System.Collections.Generic;
using System.Text;
using LogicZaliznitsya.BLL.DTO;

namespace LogicZaliznitsya.BLL.Interfaces
{
    public interface ICarriageService
    {
        void BookPlace(BookingTableDTO bookt);
        CarriageDetailDTO GetCarriage(int? id);
        void Update(CarriageDetailDTO carriage);
        void Create(CarriageDetailDTO carriage);
        void Delete(int id);
        void DeleteBooking(int id);
        CarriageDetailDTO Edit(int id);
        IEnumerable<BookingTableDTO> GetAvailableSeats(int carr_id);
        IEnumerable<CarriageDetailDTO> GetAllCarriages();
        IEnumerable<CarriageDetailDTO> GetCarriagesByTrain(int train_id);
        void Dispose();
    }
}
