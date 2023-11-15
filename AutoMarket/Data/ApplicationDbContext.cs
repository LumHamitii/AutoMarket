using AutoMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<CarModel> Cars { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "User", NormalizedName = "USER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
            );
        }
    }
}