using System.Collections.Generic;
using System.Reflection.Emit;
//using Microsoft.EntityFrameworkCore;
using WeFin.Models;

namespace WeFin.Data
{
    /*
    public class ProductDbContext : DbContext
    {
        #region Contructor
        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options)
        {
        }
        #endregion
        #region Public properties
        public DbSet<Bill> bills { get; set; }
        #endregion
        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>().HasData(GetProducts());
            base.OnModelCreating(modelBuilder);
        }
        #endregion
        #region Private methods
        private List<Bill> GetProducts()
        {
            return new List<Bill>
    {
        new Bill { Id = 1001, Name = "Laptop", Price = 20.02, Quantity = 10, Description ="This is a best gaming laptop"},
        new Bill { Id = 1002, Name = "Microsoft Office", Price = 20.99, Quantity = 50, Description ="This is a Office Application"},
        new Bill { Id = 1003, Name = "Lazer Mouse", Price = 12.02, Quantity = 20, Description ="The mouse that works on all surface"},
        new Bill { Id = 1004, Name = "USB Storage", Price = 5.00, Quantity = 20, Description ="To store 256GB of data"}
    };
        }
        #endregion
    }*/
}
