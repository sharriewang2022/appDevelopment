using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceWasmApp.Shared.Models;
using Microsoft.Extensions.Configuration;

namespace FinanceWasmApp.Server.Models
{
    public class BillDBContext : DbContext
    {
        public BillDBContext(DbContextOptions<BillDBContext> Options) : base(Options)
        {

        }
        //build table gl_bill
        public DbSet<Bill> gl_bill { get; set; }

        //build table gl_billType
        public DbSet<BillType> gl_billType { get; set; }

        //get configuration value of appsettings.json
        private IConfiguration configuration;

        /*
        public BillDBContext()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("db"));
        }
        */
    }
}