using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.DAL
{
    public class CAFFShopDbContext : DbContext
    {
        public CAFFShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Name)
                    .IsUnique();

                entity.HasMany(u => u.Roles)
                    .WithMany(r => r.Users)
                    .UsingEntity(e => e.ToTable("UserRole"));
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(
                    new Role { Id = 1, Name = "BaseUser" },
                    new Role { Id = 2, Name = "Administrator" });
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
