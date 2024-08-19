using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using onboardingAPI.Models;

namespace onboardingAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Otp> Otps {get; set;}
        public DbSet<BVNInfo> BVNInfos {get; set;}
        public DbSet<BusinessInfo> BusinessInfos {get; set;}
        public DbSet<IDInfo> IDInfos {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationships

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.Otp)
                .WithOne(o => o.AppUser)
                .HasForeignKey<Otp>(o => o.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.BVNInfo)
                .WithOne(b => b.AppUser)
                .HasForeignKey<BVNInfo>(b => b.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.BusinessInfo)
                .WithOne(b => b.AppUser)
                .HasForeignKey<BusinessInfo>(b => b.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.IDInfo)
                .WithOne(i => i.AppUser)
                .HasForeignKey<IDInfo>(i => i.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Enforce unique constraint on UserName
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Enforce unique constraint on UserName
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}