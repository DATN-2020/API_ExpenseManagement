using API_ExpenseManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Context
{
    public class ExpenseManagementContext : DbContext
    {
        public ExpenseManagementContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<TypeCategory> TypeCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TypeWallet> TypeWallets { get; set; }
        public DbSet<Income_Outcome> Income_Outcomes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<IncomeContact> IncomeContacts { get; set; }
        public DbSet<API_ExpenseManagement.Models.UserCategory> UserCategory { get; set; }
        public DbSet<API_ExpenseManagement.Models.Login> Login { get; set; }
        public DbSet<API_ExpenseManagement.Models.CreateWallet> CreateWallet { get; set; }
        public DbSet<API_ExpenseManagement.Models.Transfers> Transfers { get; set; }
        public DbSet<API_ExpenseManagement.Models.GetWallet> GetWallet { get; set; }
        public DbSet<API_ExpenseManagement.Models.GetCategory> GetCategory { get; set; }
        public DbSet<API_ExpenseManagement.Models.Budget> Budget { get; set; }
        public DbSet<API_ExpenseManagement.Models.Bill> Bill { get; set; }
        public DbSet<API_ExpenseManagement.Models.Periodic> Periodic { get; set; }
        public DbSet<API_ExpenseManagement.Models.Custom> Custom { get; set; }
    }
}
