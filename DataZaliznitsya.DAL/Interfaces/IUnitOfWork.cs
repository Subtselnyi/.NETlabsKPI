using System;
using System.Collections.Generic;
using System.Text;
using DataZaliznitsya.DAL.Entities;

namespace DataZaliznitsya.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Administration> Administartions { get; }
        IRepository<BookingTable> BookingTables { get; }
        IRepository<CarriageDetail> CarriageDetails { get; }
        IRepository<Cart> Carts { get; }
        IRepository<TrainDetail> TrainDetails { get; }
        void Save();
    }
}
