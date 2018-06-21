using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NovaZaliznitsya.Models
{
    public class CarriageViewModel
    {
        public int Id { get; set; }

        public int Train_Id { get; set; }

        public string Carriage_Desc { get; set; }

        public int Amount { get; set; }

        public TrainViewModel TrainViewModel
        {
            get => default(TrainViewModel);
            set
            {
            }
        }
    }
}