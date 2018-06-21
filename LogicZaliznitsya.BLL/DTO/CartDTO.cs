using System;
using System.Collections.Generic;
using System.Text;

namespace LogicZaliznitsya.BLL.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }

        public int Train_Id { get; set; }

        public int Carriage_Id { get; set; }

        public int Seat_Num { get; set; }

        public int User_Id { get; set; }

        public DateTime Date { get; set; }
    }
}
