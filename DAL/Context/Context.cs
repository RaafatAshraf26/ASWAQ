using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<WorkIn> WorkIn { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => new { o.CustomerID, o.OrderDate });

            modelBuilder.Entity<WorkIn>().HasKey(w => new { w.EID, w.SID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
