using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Currency
    {
        [Key]
        public int Id_Cur { get; set; }
        public string Name_Cur { get; set; }
        public string Image_Cur { get; set; }
        public string Symbol { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}
