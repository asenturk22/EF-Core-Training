using Microsoft.EntityFrameworkCore;

namespace Code_First
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Kod üzerinden migrate işlemi. 
            ECommerceDbContext context = new ECommerceDbContext();
            await context.Database.MigrateAsync();
        }
    }

    // Context 
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Product> Proudcts { get; set; } // Product Tablosu 
        public DbSet<Customer> Customers { get; set; } // Customer Tablosu 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-G27PV8J\\SQLEXPRESS; initial Catalog=ECommerceDb; integrated Security=true; ");
        }
    }


    // Entity
    public class Product
    {
        public int Id { get; set; }         // Product.Id sütunu
        public string Name { get; set; }    // Product.Name sütunu
        public int Quantity { get; set; }   // Product.Quantity sütunu
        public float Price { get; set; }    // Product.Price sütunu
    }

    public class Customer
    {
        public int Id { get; set; }                 // Customer.Id
        public string FirstName { get; set; }       // Customer.FirstName
        public string LastName { get; set; }        // Customer.LastName

    }
}


/*
PMC : Tools
PM> add-migration mig1
Build started...
Build succeeded.

veya 
CLI : Design  paketi yüklenmeli 
C:\ECommerce> dotnet ef migrations add mig1


*/