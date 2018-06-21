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
    class TrainDetailRepository : IRepository<TrainDetail>
    {
        private NovaZalContext db;

        public TrainDetailRepository(NovaZalContext context)
        {
            this.db = context;
        }

        public IQueryable<TrainDetail> GetAll()
        {
            return db.TrainDetails;
        }

        public TrainDetail Get(int id)
        {
            return db.TrainDetails.Find(id);
        }

        public void Create(TrainDetail book)
        {
            db.TrainDetails.Add(book);
        }

        public void Update(TrainDetail book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IQueryable<TrainDetail> Find(Func<TrainDetail, Boolean> predicate)
        {
            return db.TrainDetails.Where(predicate).ToList().AsQueryable();
        }

        public void Delete(int id)
        {
            TrainDetail book = db.TrainDetails.Find(id);
            if (book != null)
                db.TrainDetails.Remove(book);
        }
    }
}
