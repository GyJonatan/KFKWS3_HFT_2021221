using KFKWS3_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

            Brand bmw = new Brand() { Id = 1, Name = "BMW" };
            Brand ford = new Brand() { Id = 2, Name = "Ford" };
            Brand volkswagen = new Brand() { Id = 3, Name = "Volkswagen" };
            Brand volvo = new Brand() { Id = 4, Name = "Volvo" };

            Car bmw_1 = new Car() { Id = 1, BrandId = bmw.Id, BasePrice = 10000, Model = "I3" };
            Car bmw_2 = new Car() { Id = 2, BrandId = bmw.Id, BasePrice = 15000, Model = "M3" };
            Car bmw_3 = new Car() { Id = 3, BrandId = bmw.Id, BasePrice = 30000, Model = "X3" };
            Car ford_1 = new Car() { Id = 4, BrandId = ford.Id, BasePrice = 10000, Model = "Focus" };
            Car ford_2 = new Car() { Id = 5, BrandId = ford.Id, BasePrice = 20000, Model = "CMax" };
            Car ford_3 = new Car() { Id = 6, BrandId = ford.Id, BasePrice = 26000, Model = "Mondeo" };
            Car volkswagen_1 = new Car() { Id = 7, BrandId = volkswagen.Id, BasePrice = 8000, Model = "Polo" };
            Car volkswagen_2 = new Car() { Id = 8, BrandId = volkswagen.Id, BasePrice = 18000, Model = "Passat" };
            Car volkswagen_3 = new Car() { Id = 9, BrandId = volkswagen.Id, BasePrice = 28000, Model = "Arteon" };
            Car volvo_1 = new Car() { Id = 10, BrandId = volvo.Id, BasePrice = 20000, Model = "XC30" };
            Car volvo_2 = new Car() { Id = 11, BrandId = volvo.Id, BasePrice = 25000, Model = "XC60" };
            Car volvo_3 = new Car() { Id = 12, BrandId = volvo.Id, BasePrice = 30000, Model = "XC90" };

            Leasing telekom = new Leasing() { Id = 1, CompanyName = "Telekom" };
            Leasing vodafone = new Leasing() { Id = 2, CompanyName = "Vodafone" };
            Leasing telenor = new Leasing() { Id = 3, CompanyName = "Telenor" };

            modelBuilder.Entity<Brand>().HasData(bmw, ford, volkswagen, volvo);
            modelBuilder.Entity<Car>().HasData(
                bmw_1, bmw_2, bmw_3,
                ford_1, ford_2, ford_3,
                volkswagen_1, volkswagen_2, volkswagen_3,
                volvo_1, volvo_2, volvo_3);
            modelBuilder.Entity<Leasing>().HasData(telekom, vodafone, telenor);
        }

    }
}
