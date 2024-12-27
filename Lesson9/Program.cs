using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


#region OnConfiguring ile Konfigurasyon Ayarlarını Gerçekleştirmek 
// EF Core tool'unu yapılandırmak için kullandığımız bir metottur. 
// Context nesnesinde override edilerek kullanılmaktadır. 


#region Basit Düzeyde Entity Tanımlama Kuralları
// EF Core, her tablonun default olarak bir PK kolonu olması gerektiğini kabul eder. 
// PK kolonunu temsil eden bir property tanımlanmadığında hata verecektir. 

#endregion

#region Tablo Adını Belirleme

#endregion

namespace Lesson9
{
    internal class Program
    {
        static void Main(string[] args)
        {


        }
    }


    public class ETicaretContext : DbContext
    {
        public DbSet<Urun> Urunler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Provider  : .SqlServer
            // ConnectionString
            // Lazy Loading 
            // vb.

            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-G27PV8J\\SQLEXPRESS; " +
                "Database=ETicaretDB;  " +
                "integrated Security = true;"
                );


        }
    }

    public class Urun
    {
        // public int Id { get; set; }      P.K kabul eder veya
        // public int ID { get; set; }      P.K kabul eder veya
        // public int UrunId { get; set; }  P.K kabul eder veya 
        public int UrunID { get; set; }     // P.K
    }
}



