using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class EndSavingWallet
    {
        [Key]
        public int id_end { get; set; }
        public string id_saving { get; set; }
    }
}
