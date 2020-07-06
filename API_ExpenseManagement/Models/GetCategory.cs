using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class GetCategory
    {
        [Key]
        public int userId { get; set; }
        public int idType { get; set; }
    }
}
