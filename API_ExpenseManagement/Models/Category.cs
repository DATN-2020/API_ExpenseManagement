using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Category
    {
        [Key]
        public int Id_Cate { get; set; }
        public string NameCate { get; set; }
        public string ImageCate { get; set; }
        public int Id_type { get; set; }
        //public ICollection<Income_Outcome> Income_Outcomes { get; set; }
        //public ICollection<UserCategory> UserCategories { get; set; }
        //public ICollection<Budget> Budgets { get; set; }
        //public ICollection<Bill> Bills { get; set; }
        //public ICollection<Periodic> Periodics { get; set; }
    }
}
