using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Transfers
    {
        [Key]
        public int idTransfers { get; set; }
        public int id_chuyen { get; set; }
        public int id_nhan { get; set; }
        public float amount { get; set; }
        public string desciption { get; set; }
        public DateTime date { get; set; }
        public string Id_type { get; set; }
    }
}
