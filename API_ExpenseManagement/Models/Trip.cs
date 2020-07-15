using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Trip
    {
        [Key]
        public int Id_Trip { get; set; }
        public string Name_Trip { get; set; }
        //public ICollection<Income_Outcome> Income_Outcomes { get; set; }
    }
}
