using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Summary
    {
        [Key]
        public int id_Summary { get; set; }
        public float beginBalance { get; set; }
        public float endBalance { get; set; }
        public float netBalance { get; set; }
        public float totalIncome { get; set; }
        public float totalOutcome { get; set; }
        public float totalLoan { get; set; }
        public float totalBorrow { get; set; }
        public float totalOther { get; set; }
        public float totalIncome_Outcome { get; set; }
        public string date { get; set; }
        public string id_Come { get; set; }
        public string id_wallet { get; set; }
    }
}
