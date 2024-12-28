using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persisting_The_Data
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Veri Nasıl Eklenir
            ETicaretContext context = new ETicaretContext();
            Urun urun = new()
            {
                UrunAdi = "A urunu", 
                Fiyat = 1000
            };

            #region context.AddAsync Fonksiyonu
            
            // await context.AddAsync(urun);   //Object olarak tip güvensiz olarak veri ekler. 

            #endregion

            #region context.DbSet.AddAsync Fonksiyonu  

            await context.Urunler.AddAsync(urun);   // Hangi türden veri belli  olarak tip güvenli veri ekler. 

            #endregion

            #region SaveChanges Nedir?
            // SaveChanges, insert, update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına 
            // gönderip execute eden fonksiyondur. Eğer oluşturulan fonksiyonlardan herhangi biri başarısız olursa
            // tüm işlemleri geri alır. 

            await context.SaveChangesAsync();

            #endregion


            #region EF core açısından Bir verinin eklenmesi gerektiği nasıl anlaşılır?

            ETicaretContext context2 = new ();
            Urun urun2 = new()
            {
                UrunAdi = "B ürünü",
                Fiyat = 2000
            };

            Console.WriteLine(context2.Entry(urun).State);  // Detached

            await context2.AddAsync(urun2);
            Console.WriteLine(context2.Entry(urun).State);  //added

            await context2.SaveChangesAsync();
            Console.WriteLine(context2.Entry(urun).State);  //Uncanged

            #endregion


            #region Birden fazla veri eklerken nelere dikkat edilmelidir?
            ETicaretContext context3 = new();
            Urun urun3 = new()
            {
                UrunAdi = "C ürünü",
                Fiyat = 3000
            };

            await context3.AddAsync(urun);
            await context3.SaveChangesAsync();

            await context3.AddAsync(urun2);
            await context3.SaveChangesAsync();

            await context3.AddAsync(urun3);
            await context3.SaveChangesAsync();

            #endregion


            #region SaveChanges'i verimli kullanmak
            // SaveChanges fonksiyonu her tetiklendiğinde bir transaction oluşturulacağından 
            // EF Core ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme
            // özel Transaction veritabanı açısından ekstra maliyettir. 


            ETicaretContext context4 = new();


            Console.WriteLine(context4.Entry(urun).State);  // Detached

            await context4.AddAsync(urun2);
            Console.WriteLine(context4.Entry(urun).State);  //added

            await context4.SaveChangesAsync();
            Console.WriteLine(context4.Entry(urun).State);  //Uncanged

            #endregion


            #region AddRange
            await context.AddRangeAsync(urun);
            await context.Urunler.AddRangeAsync(urun, urun2, urun3);
            await context.SaveChangesAsync();
            #endregion

            #region Eklenen verinin Generete edilen Id'sini elde etme. 

            #endregion
            //SaveChangesAsync();
            Console.WriteLine(urun.ID);
            #endregion
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
        public int ID { get; set; }     // P.K
        public string UrunAdi { get; set; }
        public float Fiyat { get; set; }
    }
}





