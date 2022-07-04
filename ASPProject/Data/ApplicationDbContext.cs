using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASPProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<ClientAccount> ClientAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite primary keys
            modelBuilder.Entity<ClientAccount>()
                .HasKey(ca => new { ca.clientID, ca.accountNum });

            // Define foreign keys here
            modelBuilder.Entity<ClientAccount>()
                .HasOne(c => c.Client)
                .WithMany(c => c.ClientAccounts)
                .HasForeignKey(fk => new { fk.clientID })
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete

            modelBuilder.Entity<ClientAccount>()
                .HasOne(c => c.BankAccount)
                .WithMany(c => c.ClientAccounts)
                .HasForeignKey(fk => new { fk.accountNum })
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete
        }

        public class Client
        {
            [Key]
            [Required]
            public int clientID { get; set; }
            [Display(Name = "Last Name")]
            [Required]
            public string lastName { get; set; }
            [Display(Name = "First Name")]
            [Required]
            public string firstName { get; set; }
            [Display(Name = "Email")]
            [Required]
            public string email { get; set; }

            // Navigation properties
            // Child
            public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
        }
        public class BankAccount
        {
            [Key]
            public int accountNum { get; set; }
            [Display(Name = "Account Type")]
            [Required]
            public string accountType { get; set; }
            [Display(Name = "Balance")]
            [Required]
            public decimal balance { get; set; }

            // Navigation properties
            // Child
            public virtual ICollection<ClientAccount> ClientAccounts { get; set; }
        }
        public class ClientAccount
        {
            [Key, Column(Order=0)]
            public int clientID { get; set; }
            [Key, Column(Order=1)]
            public int accountNum { get; set; }

            // Navigation Properties
            // Parents
            public virtual Client Client { get; set; }
            public virtual BankAccount BankAccount { get; set; }
        }
    }
}
