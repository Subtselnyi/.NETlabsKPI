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
    
    class BookingTableRepository : IRepository<BookingTable>
    {
        private NovaZalContext db;

        public BookingTableRepository(NovaZalContext context)
        {
            this.db = context;
        }

        public IQueryable<BookingTable> GetAll()
        {
            return db.BookingTables;
        }

        public BookingTable Get(int id)
        {
            return db.BookingTables.Find(id);
        }

        public void Create(BookingTable book)
        {
            db.BookingTables.Add(book);
        }

        public void Update(BookingTable book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IQueryable<BookingTable> Find(Func<BookingTable, Boolean> predicate)
        {
            return db.BookingTables.Where(predicate).ToList().AsQueryable();
        }

        public void Delete(int id)
        {
            BookingTable book = db.BookingTables.Find(id);
            if (book != null)
                db.BookingTables.Remove(book);
        }
    }
}
