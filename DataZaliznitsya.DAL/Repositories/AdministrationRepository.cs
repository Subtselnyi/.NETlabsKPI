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
    class AdministrationRepository : IRepository<Administration>
    {
        private NovaZalContext db;

        public AdministrationRepository(NovaZalContext context)
        {
            this.db = context;
        }

        public IQueryable<Administration> GetAll()
        {
            return db.Administrations;
        }

        public Administration Get(int id)
        {
            return db.Administrations.Find(id);
        }

        public void Create(Administration book)
        {
            db.Administrations.Add(book);
        }

        public void Update(Administration book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IQueryable<Administration> Find(Func<Administration, Boolean> predicate)
        {
            return db.Administrations.Where(predicate).ToList().AsQueryable();
        }

        public void Delete(int id)
        {
            Administration book = db.Administrations.Find(id);
            if (book != null)
                db.Administrations.Remove(book);
        }
    }
}
