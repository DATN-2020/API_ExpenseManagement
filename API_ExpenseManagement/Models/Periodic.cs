﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_ExpenseManagement.Models
{
    public class Periodic
    {
        [Key]
        public int Id_Per { get; set; }
        public float Amount_Per { get; set; }
        public string Desciption { get; set; }
        public DateTime date_e { get; set; }
        public DateTime date_s { get; set; }
        public bool isComeback { get; set; }
        public bool isPay { get; set; }
        public bool isFinnish { get; set; }
        public string Id_Cate { get; set; }
        public string Id_Type { get; set; }
        public string Id_Wallet { get; set; }
        public string id_Time { get; set; }
        //public ICollection<Income_Outcome> income_Outcomes { get; set; }
    }
}
