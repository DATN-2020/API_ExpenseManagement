using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class SavingWallet
    {
        [Key]
        public int id_saving { get; set; }
        public string name_saving { get; set; }
        public string date_s { get; set; }
        public float price { get; set; }
        public float price_end { get; set; }
        public string date_e { get; set; }
        public string id_bank { get; set; }
        public bool is_Finnish { get; set; }
        public string id_user { get; set; }
    }
}
