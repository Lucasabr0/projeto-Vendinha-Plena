using Microsoft.EntityFrameworkCore;
using VendinhaPlena.API.Models;

namespace VendinhaPlena.API.Data
{
    public class VendinhaDbContext : DbContext
    {
        public VendinhaDbContext(DbContextOptions<VendinhaDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Divida> Dividas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            modelBuilder.Entity<Divida>()
                .HasOne(d => d.Cliente)
                .WithMany(c => c.Dividas)
                .HasForeignKey(d => d.ClienteId);
        }
    }
}
