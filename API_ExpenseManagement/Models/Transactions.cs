using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Transactions
    {
        [Key]
        public int id_trans { get; set; }
        public string name_trans { get; set; }
        public float price_trans { get; set; }
        public string date_trans { get; set; }
        public string id_saving { get; set; }
    }
}
