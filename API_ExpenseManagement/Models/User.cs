using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool Check_Wallet { get; set; }
        public ICollection<UserCategory> UserCategories { get; set; }
        public ICollection<Wallet> wallets { get; set; }
        //public ICollection<Budget> budgets { get; set; }
        //public virtual ICollection<Bill> bills { get; set; }
        //public virtual ICollection<Periodic> Periodics { get; set; }
    }
}
