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
        public float totalIncome_Outcome_1 { get; set; }
        public float totalIncome_Outcome_2 { get; set; }
        public float totalIncome_Outcome_3 { get; set; }
        public float totalIncome_Outcome_4 { get; set; }
        public float totalIncome_Outcome_5 { get; set; }
        public float totalIncome_Outcome_6 { get; set; }
        public float totalIncome_Outcome_7 { get; set; }
        public float totalIncome_Outcome_8 { get; set; }
        public float totalIncome_Outcome_9 { get; set; }
        public float totalIncome_Outcome_10 { get; set; }
        public float totalIncome_Outcome_11 { get; set; }
        public float totalIncome_Outcome_12 { get; set; }
        //public string date_set { get; set; }
        public string date { get; set; }
        //public string id_Come { get; set; }
        public string id_wallet { get; set; }
    }
}
