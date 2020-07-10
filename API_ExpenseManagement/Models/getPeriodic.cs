using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class getPeriodic
    {
        [Key]
        public int id_getBudget { get; set; }
        public int id_cate { get; set; }
        public int id_type { get; set; }
        public float amount_budget { get; set; }
    }
}
