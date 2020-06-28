using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Login
    {
        [Key]
        public string User_Name { get; set; }
        public string Password { get; set; }
    }
}
