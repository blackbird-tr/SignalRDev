using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRDev.Models;

namespace SignalRDev.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Message entity konfigürasyonu
            builder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Body).IsRequired();
                entity.Property(e => e.SenderId).IsRequired();
                entity.Property(e => e.ReceiverId).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
} 