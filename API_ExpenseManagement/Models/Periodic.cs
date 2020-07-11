using System;
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
        public int Id_Cate { get; set; }
        public int Id_Type { get; set; }
        public int Id_Wallet { get; set; }
    }
}
