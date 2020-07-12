using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Time_Periodic
    {
        [Key]
        public int id_Time { get; set; }
        public string desciption { get; set; }
        public DateTime date_s { get; set; }
        public DateTime date_e { get; set; }
        public ICollection<Periodic> Periodics { get; set; }
        public ICollection<Bill> bills { get; set; }
        public ICollection<Budget> budgets { get; set; }
    }
}
