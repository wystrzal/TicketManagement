using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Issue>(issue =>
            {
                issue.HasMany(x => x.Messages)
                .WithOne(x => x.Issue)
                .HasForeignKey(x => x.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

                issue.HasOne(x => x.Declarant)
                .WithMany(x => x.DeclarantedIssues)
                .HasForeignKey(x => x.DeclarantId);
            });

            builder.Entity<SupportIssues>(suppIss =>
            {
                suppIss.HasKey(x => new { x.IssueId, x.SupportId });

                suppIss.HasOne(x => x.User)
                .WithMany(x => x.SupportIssues)
                .HasForeignKey(x => x.SupportId);

                suppIss.HasOne(x => x.Issue)
                .WithMany(x => x.SupportIssues)
                .HasForeignKey(x => x.IssueId);            
            });

            builder.Entity<User>(user =>
            {
                user.HasOne(x => x.Departament)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.DepartamentId);
            });
        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SupportIssues> SupportedIssues { get; set; }
        public DbSet<Departament> Departaments { get; set; }
    }
}
