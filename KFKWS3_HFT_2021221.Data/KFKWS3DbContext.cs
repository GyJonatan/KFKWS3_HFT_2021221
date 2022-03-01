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


            List<Leasing> leasingList = new List<Leasing>()
            {
                new Leasing() { Id = 1, HQLocation ="Budapest AAA Auto u. 1.", Name = "AAA Auto", Budget = 10000000 },
                new Leasing() { Id = 2, HQLocation ="Budapest Mr. Rent u. 1.", Name = "Mr. Rent", Budget = 5000000 },
                new Leasing() { Id = 3, HQLocation ="Debrecen CarRental u. 1.",Name = "CarRental", Budget = 2500000 }
            };


            List<Brand> brandList = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "BMW", LeasingId = leasingList.Find(x => x.Name == "AAA Auto").Id },
                new Brand() { Id = 2, Name = "Ford", LeasingId = leasingList.Find(x => x.Name == "AAA Auto").Id },
                new Brand() { Id = 3, Name = "Volkswagen", LeasingId = leasingList.Find(x => x.Name == "Mr. Rent").Id },
                new Brand() { Id = 4, Name = "Volvo", LeasingId = leasingList.Find(x => x.Name == "CarRental").Id }
            };


            List<Car> carList = new List<Car>()
            {
                new Car() { Id = 1, BrandId = brandList.Find(x => x.Name == "BMW").Id, BasePrice = 10000, Model = "I3" },
                new Car() { Id = 2, BrandId = brandList.Find(x => x.Name == "BMW").Id, BasePrice = 15000, Model = "M3" },
                new Car() { Id = 3, BrandId = brandList.Find(x => x.Name == "BMW").Id, BasePrice = 30000, Model = "X3" },
                new Car() { Id = 4, BrandId = brandList.Find(x => x.Name == "Ford").Id, BasePrice = 10000, Model = "Focus" },
                new Car() { Id = 5, BrandId = brandList.Find(x => x.Name == "Ford").Id, BasePrice = 20000, Model = "CMax" },
                new Car() { Id = 6, BrandId = brandList.Find(x => x.Name == "Ford").Id, BasePrice = 26000, Model = "Mondeo" },
                new Car() { Id = 7, BrandId = brandList.Find(x => x.Name == "Ford").Id, BasePrice = 8000, Model = "Polo" },
                new Car() { Id = 8, BrandId = brandList.Find(x => x.Name == "Volkswagen").Id, BasePrice = 18000, Model = "Passat" },
                new Car() { Id = 9, BrandId = brandList.Find(x => x.Name == "Volkswagen").Id, BasePrice = 28000, Model = "Arteon" },
                new Car() { Id = 10, BrandId = brandList.Find(x => x.Name == "Volvo").Id, BasePrice = 20000, Model = "XC40" },
                new Car() { Id = 11, BrandId = brandList.Find(x => x.Name == "Volvo").Id, BasePrice = 25000, Model = "XC60" },
                new Car() { Id = 12, BrandId = brandList.Find(x => x.Name == "Volvo").Id, BasePrice = 30000, Model = "XC90" }
            };


            


            modelBuilder.Entity<Brand>().HasData(brandList);
            modelBuilder.Entity<Car>().HasData(carList);
            modelBuilder.Entity<Leasing>().HasData(leasingList);
        }
    }
}
