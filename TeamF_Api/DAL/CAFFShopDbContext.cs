using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.DAL
{
    public class CAFFShopDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public CAFFShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.UserName).IsUnique();

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

            modelBuilder.Entity<Comment>()
               .HasOne<CaffEntity>(s => s.CaffEntity)
               .WithMany(c => c.Comments)
               .HasForeignKey(u => u.CaffEntityId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CaffEntity> CaffEntity { get; set; }
        public DbSet<Img> Img { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
