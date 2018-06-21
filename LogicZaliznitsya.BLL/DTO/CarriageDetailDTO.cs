using System;
using System.Collections.Generic;
using System.Text;

namespace LogicZaliznitsya.BLL.DTO
{
    public class CarriageDetailDTO
    {
        public int Id { get; set; }

        public int Train_Id { get; set; }

        public string Carriage_Desc { get; set; }

        public int Amount { get; set; }
    }
}
