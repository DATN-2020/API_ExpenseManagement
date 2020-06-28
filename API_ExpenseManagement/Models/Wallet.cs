using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Wallet
    {
        [Key]
        public int Id_Wallet { get; set; }
        public string Name_Wallet { get; set; }
        public string Description { get; set; }
        public int Id_Type_Wallet { get; set; }
        public int User_Id { get; set; }

    }
}
