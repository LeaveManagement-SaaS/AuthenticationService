using System;
using System.Collections.Generic;
using System.Text;
using AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);
        }

    }
}
