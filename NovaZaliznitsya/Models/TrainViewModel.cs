using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NovaZaliznitsya.Models
{
    public class TrainViewModel
    {
        public int Id { get; set; }

        public string Train_Name { get; set; }

        public string From_City { get; set; }

        public string To_City { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public string Train_Desc { get; set; }
    }
}