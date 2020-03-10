using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebPrsTest.Models;

namespace WebPrsTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model) {
            model.Entity<User>(u => {
                u.Property(x => x.Username).HasMaxLength(30).IsRequired();
                u.HasIndex(x => x.Username).IsUnique();
            });
            model.Entity<Vendor>(v => {
                v.HasIndex(x => x.Code).IsUnique();
            });
            model.Entity<Product>(p => {
                p.HasIndex(x => x.PartNbr).IsUnique();
                p.Property(x => x.Price).HasColumnType("decimal(11,2)").IsRequired();
            });
            model.Entity<Request>(r => {
                r.Property(x => x.Total).HasColumnType("decimal(11,2)").IsRequired();
            });
        }

        public DbSet<WebPrsTest.Models.User> User { get; set; }

        public DbSet<WebPrsTest.Models.Vendor> Vendor { get; set; }

        public DbSet<WebPrsTest.Models.Product> Product { get; set; }

        public DbSet<WebPrsTest.Models.Request> Request { get; set; }

        public DbSet<WebPrsTest.Models.RequestLine> RequestLine { get; set; }
    }
}
