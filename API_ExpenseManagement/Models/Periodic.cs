using System.ComponentModel.DataAnnotations;

namespace API_ExpenseManagement.Models
{
    public class Periodic
    {
        [Key]
        public int Id_Per { get; set; }
        public float Amount_Per { get; set; }
        public string Desciption { get; set; }
        public int Id_Cate { get; set; }
        public int Id_Wallet { get; set; }
        public int Id_Custom { get; set; }
    }
}
