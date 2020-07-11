using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Budget
    {
        [Key]
        public int Id_Budget { get; set; }
        public float Amount_Budget { get; set; }
        public float Remain { get; set; } //số tiền còn lại
        public DateTime time_s { get; set; }
        public DateTime time_e { get; set; }
        public bool isFinnish { get; set; }
        public int Id_Cate { get; set; }
        public int Id_Wallet { get; set; }
        public int Id_type { get; set; }
    }
}
