﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class getIncome
    {
        [Key]
        public int id_getIncome { get; set; }
        public int id_cate { get; set; }
        public int id_type { get; set; }
        public float total_Icome { get; set; }
        public float total_Outcome { get; set; }
    }
}
