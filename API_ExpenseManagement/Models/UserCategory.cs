using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class UserCategory
    {
        [Key]
        public int Id_UserCategory { get; set; }
    }
}
