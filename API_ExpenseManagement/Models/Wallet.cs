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
        public float Amount_Wallet { get; set; }
        public string Description { get; set; }
        public int Id_Type_Wallet { get; set; }
        public int User_Id { get; set; }
        public ICollection<Income_Outcome> Income_Outcomes { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Periodic> Periodics { get; set; }

    }
}
