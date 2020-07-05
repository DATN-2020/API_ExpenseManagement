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
        public int Id_Type { get; set; }
        public ICollection<Income_Outcome> Income_Outcomes { get; set; }
        public ICollection<UserCategory> UserCategories { get; set; }
    }
}
