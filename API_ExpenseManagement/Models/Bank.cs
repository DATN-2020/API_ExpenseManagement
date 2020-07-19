using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class Bank
    {
        [Key]
        public int Id_Bank { get; set; }
        public string Name_Bank { get; set; }
        public float Interest { get; set; }

    }
}
