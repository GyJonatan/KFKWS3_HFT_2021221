using KFKWS3_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KFKWS3_HFT_2021221.Data
{

    public class KFKWS3DbContext : DbContext
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Leasing> Leasings { get; set; }

        public KFKWS3DbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;
                            AttachDbFilename=|DataDirectory|\KFKWS3_HFT_2021221.Database.mdf;
                            Integrated Security=True;MultipleActiveResultSets=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasOne(car => car.Brand)
                      .WithMany(brand => brand.Cars)
                      .HasForeignKey(car => car.BrandId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasOne(brand => brand.Leasing)
                      .WithMany(leasing => leasing.Brands)
                      .HasForeignKey(brand => brand.LeasingId)
                      .OnDelete(DeleteBehavior.NoAction);
            });



            List<Brand> brandList = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "BMW" },
                new Brand() { Id = 2, Name = "Ford" },
                new Brand() { Id = 3, Name = "Volkswagen" },
                new Brand() { Id = 4, Name = "Volvo" }
            };

            List<Car> carList = new List<Car>()
            {
                new Car() { Id = 1, BrandId = GetBrandId(brandList,"BMW"), BasePrice = 10000, Model = "I3" },
                new Car() { Id = 2, BrandId = GetBrandId(brandList,"BMW"), BasePrice = 15000, Model = "M3" },
                new Car() { Id = 3, BrandId = GetBrandId(brandList,"BMW"), BasePrice = 30000, Model = "X3" },
                new Car() { Id = 4, BrandId = GetBrandId(brandList,"Ford"), BasePrice = 10000, Model = "Focus" },
                new Car() { Id = 5, BrandId = GetBrandId(brandList,"Ford"), BasePrice = 20000, Model = "CMax" },
                new Car() { Id = 6, BrandId = GetBrandId(brandList,"Ford"), BasePrice = 26000, Model = "Mondeo" },
                new Car() { Id = 7, BrandId = GetBrandId(brandList,"Volkswagen"), BasePrice = 8000, Model = "Polo" },
                new Car() { Id = 8, BrandId = GetBrandId(brandList,"Volkswagen"), BasePrice = 18000, Model = "Passat" },
                new Car() { Id = 9, BrandId = GetBrandId(brandList,"Volkswagen"), BasePrice = 28000, Model = "Arteon" },
                new Car() { Id = 10, BrandId = GetBrandId(brandList,"Volvo"), BasePrice = 20000, Model = "XC40" },
                new Car() { Id = 11, BrandId = GetBrandId(brandList,"Volvo"), BasePrice = 25000, Model = "XC60" },
                new Car() { Id = 12, BrandId = GetBrandId(brandList,"Volvo"), BasePrice = 30000, Model = "XC90" }
            };

            List<Leasing> leasingList = new List<Leasing>()
            {
                new Leasing() { Id = 1, Name = "AAA Auto" },
                new Leasing() { Id = 2, Name = "Mr. Rent" },
                new Leasing() { Id = 3, Name = "CarRental" }
            };
            


            modelBuilder.Entity<Brand>().HasData(brandList);
            modelBuilder.Entity<Car>().HasData(carList);
            modelBuilder.Entity<Leasing>().HasData(leasingList);
        }

        public static int GetBrandId(List<Brand> list, string name)
        {
            return list.Find(x => x.Name == name).Id;
        }

    }
}
