using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Contact
    {
        [Key]
        public int Id_Contact { get; set; }
        public string Name_Contact { get; set; }
        public string PhoneNumber { get; set; }
        //public ICollection<Loan> Loans { get; set; }
        //public ICollection<IncomeContact> IncomeContacts { get; set; }
    }
}
