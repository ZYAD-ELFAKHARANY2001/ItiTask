using Ecommerce.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Context
{
    public class EcommerceDbcontext: IdentityDbContext
    {

        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Account>()
        //        .HasMany(a => a.Roles)
        //        .WithMany(r => r.Accounts)
        //        .UsingEntity("AccountRole",
        //            j => j.HasOne(typeof(Account)).WithMany().HasForeignKey("AccountId").HasPrincipalKey(nameof(Account.Id)),
        //            r => r.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
        //            j => j.HasKey("AccountId", "RoleId"));

        //}
        //public EcommerceContext() { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-3LOCF94\\SQLEXPRESS;Initial Catalog=labMVC;Integrated Security=True;Encrypt=False");
        //    base.OnConfiguring(optionsBuilder);
        //}
        public EcommerceDbcontext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}
