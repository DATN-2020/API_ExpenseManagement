using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class TypeWallet
    {
        [Key]
        public int Id_Type_Wallet { get; set; }
        public int Name_Type_Wallet { get; set; }
        public int Image_Type_Wallet { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}
