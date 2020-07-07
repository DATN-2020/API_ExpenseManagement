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
        public string time { get; set; }
        public bool repeat { get; set; }
        public int Id_Cate { get; set; }
        public int Id_Wallet { get; set; }
        //public int User_Id { get; set; }
    }
}
