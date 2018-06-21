using System;
using System.Collections.Generic;
using System.Text;

namespace DataZaliznitsya.DAL.Entities
{
    public class TrainDetail
    {
        public TrainDetail()
        {
            CarriageDetails = new HashSet<CarriageDetail>();
        }

        public int Id { get; set; }

        public string Train_Name { get; set; }

        public string From_City { get; set; }

        public string To_City { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string Train_Desc { get; set; }


        public virtual ICollection<CarriageDetail> CarriageDetails { get; set; }
    }
}
