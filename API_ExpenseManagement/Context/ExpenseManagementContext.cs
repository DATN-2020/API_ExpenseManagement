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
    }
}
