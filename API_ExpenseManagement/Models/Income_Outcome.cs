using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Income_Outcome
    {
        [Key]
        public int Id_come { get; set; }
        public float Amount { get; set; }
        public string Date_come { get; set; }
        public int Description_come { get; set; }
        public bool Is_Come { get; set; }
        public ICollection<IncomeContact> IncomeContacts { get; set; }
    }
}
