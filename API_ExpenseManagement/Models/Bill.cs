using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Bill
    {
        [Key]
        public int Id_Bill { get; set; }
        public float Amount_Bill { get; set; }
        public string Desciption { get; set; }
        public int Id_Cate { get; set; }
        public int Id_Wallet { get; set; }
        public int Id_Custom { get; set; }
    }
}
