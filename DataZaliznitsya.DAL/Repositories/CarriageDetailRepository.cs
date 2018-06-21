using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using DataZaliznitsya.DAL.Entities;
using DataZaliznitsya.DAL.EF;
using DataZaliznitsya.DAL.Interfaces;
using System.Linq;

namespace DataZaliznitsya.DAL.Repositories
{
    
    class CarriageDetailRepository : IRepository<CarriageDetail>
    {
        private NovaZalContext db;

        public CarriageDetailRepository(NovaZalContext context)
        {
            this.db = context;
        }

        public IQueryable<CarriageDetail> GetAll()
        {
            return db.CarriageDetails;
        }
        
        public CarriageDetail Get(int id)
        {
            return db.CarriageDetails.Find(id);
        }

        public void Create(CarriageDetail book)
        {
            db.CarriageDetails.Add(book);
        }

        public void Update(CarriageDetail book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IQueryable<CarriageDetail> Find(Func<CarriageDetail, Boolean> predicate)
        {
            return db.CarriageDetails.Where(predicate).ToList().AsQueryable();
        }

        public void Delete(int id)
        {
            CarriageDetail book = db.CarriageDetails.Find(id);
            if (book != null)
                db.CarriageDetails.Remove(book);
        }
    }
}
