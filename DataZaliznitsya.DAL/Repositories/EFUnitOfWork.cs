using System;
using DataZaliznitsya.DAL.EF;
using DataZaliznitsya.DAL.Interfaces;
using DataZaliznitsya.DAL.Entities;
 
namespace DataZaliznitsya.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private NovaZalContext db;
        private AdministrationRepository administrationRepository;
        private BookingTableRepository bookingTableRepository;
        private CarriageDetailRepository carriageDetailRepository;
        private CartRepository cartRepository;
        private TrainDetailRepository trainDetailRepository;


        public EFUnitOfWork(string connectionString)
        {
            db = new NovaZalContext(connectionString);
        }
        public IRepository<Administration> Administartions
        {
            get
            {
                if (administrationRepository == null)
                    administrationRepository = new AdministrationRepository(db);
                return administrationRepository;
            }
        }
        public IRepository<BookingTable> BookingTables
        {
            get
            {
                if (bookingTableRepository == null)
                    bookingTableRepository = new BookingTableRepository(db);
                return bookingTableRepository;
            }
        }
        public IRepository<CarriageDetail> CarriageDetails
        {
            get
            {
                if (carriageDetailRepository == null)
                    carriageDetailRepository = new CarriageDetailRepository(db);
                return carriageDetailRepository;
            }
        }
        public IRepository<Cart> Carts
        {
            get
            {
                if (cartRepository == null)
                    cartRepository = new CartRepository(db);
                return cartRepository;
            }
        }
        public IRepository<TrainDetail> TrainDetails
        {
            get
            {
                if (trainDetailRepository == null)
                    trainDetailRepository = new TrainDetailRepository(db);
                return trainDetailRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}