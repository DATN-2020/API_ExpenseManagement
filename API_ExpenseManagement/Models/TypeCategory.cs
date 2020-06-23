using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class TypeCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name_Type { get; set; }
        public string Image_Type { get; set; }
        public string TypeExpense { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Income_Outcome> Income_Outcomes { get; set; }
    }
}
