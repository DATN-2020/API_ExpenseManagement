using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Loan
    {
        [Key]
        public int Id_Loan { get; set; }
        public string Name_Loan { get; set; }
        public string Date_Pay { get; set; }
        public int ContactId_contact { get; set; }
        //public ICollection<Income_Outcome> Income_Outcomes { get; set; }
    }
}
