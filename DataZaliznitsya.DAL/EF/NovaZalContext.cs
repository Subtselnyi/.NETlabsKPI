using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using DataZaliznitsya.DAL.Entities;

namespace DataZaliznitsya.DAL.EF
{
    class NovaZalContext : DbContext
    {
        public NovaZalContext()
            : base("name=UkrZal")
        {
        }
        public NovaZalContext(string connectionString)
            : base(connectionString)
        {
        }

        public  DbSet<Administration> Administrations { get; set; }
        public  DbSet<BookingTable> BookingTables { get; set; }
        public  DbSet<CarriageDetail> CarriageDetails { get; set; }
        public  DbSet<Cart> Carts { get; set; }
        public  DbSet<TrainDetail> TrainDetails { get; set; }

        static NovaZalContext()
        {
            Database.SetInitializer<NovaZalContext>(new StoreDbInitializer());
        }

        public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<NovaZalContext>
        {
            protected override void Seed(NovaZalContext db)
            {
                db.TrainDetails.Add(new TrainDetail { Train_Name = "Khmelnytskiy Express", From_City = "Khmelnytskyi", To_City = "Kyiv", DepartureDate = DateTime.Parse("2018/06/06 19:00:00"), ArrivalDate = DateTime.Parse("2018/06/06 23:00:00"), Train_Desc = "Intercity+" });
                db.CarriageDetails.Add(new CarriageDetail { Train_Id = 1, Carriage_Desc = "Carrige with buffet", Amount = 40 });
                db.CarriageDetails.Add(new CarriageDetail { Train_Id = 1, Carriage_Desc = "Carrige with buffet", Amount = 40 });                
                db.SaveChanges();
            }
        }

    }
}
