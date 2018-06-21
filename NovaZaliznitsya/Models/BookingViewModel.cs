using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NovaZaliznitsya.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public int Seat_Num { get; set; }

        public DateTime Date { get; set; }

        public int Carriage_Id { get; set; }

        public string Description { get; set; }

        public string NAME { get; set; }

        public string EMAIL { get; set; }

        public CarriageViewModel CarriageViewModel
        {
            get => default(CarriageViewModel);
            set
            {
            }
        }
    }
}