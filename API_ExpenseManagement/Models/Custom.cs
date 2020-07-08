using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Custom
    {
        [Key]
        public int Id_Custom { get; set; }
        public string Prequency { get; set; }
        public ICollection<Periodic> Periodics { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
