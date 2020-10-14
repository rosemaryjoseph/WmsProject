using BusinessService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessService.Data
{
    public partial class BusinessServiceDbContext : DbContext
    {
        public BusinessServiceDbContext(DbContextOptions<BusinessServiceDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().Property<bool>("IsDeleted");
            modelBuilder.Entity<Customer>().HasQueryFilter(post => EF.Property<bool>(post, "IsDeleted") == false);

            modelBuilder.Entity<Quote>().Property<bool>("IsDeleted");
            modelBuilder.Entity<Quote>().HasQueryFilter(post => EF.Property<bool>(post, "IsDeleted") == false);
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }

    }
}