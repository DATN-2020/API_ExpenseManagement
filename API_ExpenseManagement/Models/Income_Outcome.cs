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
        public string Description_come { get; set; }
        public bool Is_Come { get; set; }
        public string CategoryId_Cate { get; set; }
        public string LoanId_Loan { get; set; }
        public string TripId_Trip { get; set; }
        public string Id_type { get; set; }
        public string WalletId_Wallet { get; set; }
        public string Id_Bill { get; set; }
        public string Id_Budget { get; set; }
        public string Id_Per { get; set; }
        //public ICollection<IncomeContact> IncomeContacts { get; set; }
    }
}
