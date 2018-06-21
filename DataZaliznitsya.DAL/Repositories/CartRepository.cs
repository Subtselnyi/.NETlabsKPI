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
    class CartRepository : IRepository<Cart>
    {
        private NovaZalContext db;

        public CartRepository(NovaZalContext context)
        {
            this.db = context;
        }

        public IQueryable<Cart> GetAll()
        {
            return db.Carts;
        }

        public Cart Get(int id)
        {
            return db.Carts.Find(id);
        }

        public void Create(Cart book)
        {
            db.Carts.Add(book);
        }

        public void Update(Cart book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IQueryable<Cart> Find(Func<Cart, Boolean> predicate)
        {
            return db.Carts.Where(predicate).ToList().AsQueryable();
        }

        public void Delete(int id)
        {
            Cart book = db.Carts.Find(id);
            if (book != null)
                db.Carts.Remove(book);
        }
    }
}
