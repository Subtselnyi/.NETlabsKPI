using System;
using System.Collections.Generic;
using System.Text;

namespace DataZaliznitsya.DAL.Entities
{
    public class CarriageDetail
    {
        public CarriageDetail()
        {
            BookingTables = new HashSet<BookingTable>();
        }

        public int Id { get; set; }

        public int Train_Id { get; set; }

        public string Carriage_Desc { get; set; }

        public int Amount { get; set; }

        public virtual ICollection<BookingTable> BookingTables { get; set; }

        public virtual TrainDetail TrainDetail { get; set; }

    }
}
