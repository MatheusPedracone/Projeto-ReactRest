using Chimera_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Chimera_v2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>(entity =>
                entity.HasOne(x => x.Adress)
                .WithOne(x => x.Client)
                .HasForeignKey<Adress>(x => x.ClientId));

            builder.Entity<User>()
              .HasIndex(user => user.Username)
                .IsUnique();
        }
    }
}