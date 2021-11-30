using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;

namespace TeamF_Api.DAL
{
    public class CAFFShopDbContext : IdentityDbContext<User, Role, Guid>
    {
        public CAFFShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role { Id = Guid.NewGuid(), Name = SecurityConstants.BaseUserRole, NormalizedName = SecurityConstants.BaseUserRole.ToUpper() },
                    new Role { Id = Guid.NewGuid(), Name = SecurityConstants.AdminRole, NormalizedName = SecurityConstants.AdminRole.ToUpper() }
                    );

            modelBuilder.Entity<Comment>()
               .HasOne<CaffEntity>(s => s.CaffEntity)
               .WithMany(c => c.Comments)
               .HasForeignKey(u => u.CaffEntityId)
               .OnDelete(DeleteBehavior.Restrict);

        }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CaffEntity> CaffEntity { get; set; }
        public DbSet<Img> Img { get; set; }
        public DbSet<Tag> Tag { get; set; }
    }
}
