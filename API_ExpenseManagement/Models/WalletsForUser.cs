using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class WalletsForUser
    {
        [Key]
        public int User_Id { get; set; }
    }
}
