﻿using AutoMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPhoto> Photos { get; set; }
        public DbSet<CarBrand> Brands { get; set; }
        public DbSet<CarColor> Colors { get; set; }
        public DbSet<CarCondition> Condition { get; set; }
        public DbSet<CarFuelType> FuelTypes { get; set; }
        public DbSet<CarMileage> Mileages { get; set; }
        public DbSet<CarModel> Models { get; set; }
        public DbSet<CarSeats> Seats { get; set; }
        public DbSet<CarTransmissionType> TransmissionTypes { get; set; }
        public DbSet<CarVersion> Versions { get; set; }
        public DbSet<Motorcycle>Motorcycles { get; set; }
        public DbSet<MotorcycleBrand> MotorcycleBrands { get; set; }
        public DbSet<MotorcycleColor> MotorcycleColors { get; set; }
        public DbSet<MotorcycleCondition> MotorcycleConditions { get; set; }
        public DbSet<MotorcycleFuelType> MotorcycleFuelTypes { get; set; }
        public DbSet<MotorcycleMileage> MotorcycleMileages { get; set; }
        public DbSet<MotorcycleModel> MotorcycleModels { get; set; }
        public DbSet<MotorcycleTransmission> MotorcycleTransmissions { get; set; }
        public DbSet<MotorcycleType> MotorcycleTypes { get; set; }
        public DbSet<MotorcycleYear> MotorcycleYears { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        // base.OnModelCreating(modelBuilder);

        // Define roles
        //  modelBuilder.Entity<IdentityRole>().HasData(
        //   new IdentityRole { Name = "User", NormalizedName = "USER" },
        //  new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
        // );
        //  }
        
    }
}