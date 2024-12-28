using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Persisting_The_Data_Update
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Veri Nasıl Güncellenir?
            ETicaretContext context = new();

            //urun.ID = 3 olan ürünü getir. 
            Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.ID == 3);
            // Gelen ürünü güncelleyelim. 
            urun.UrunAdi = "C ürünü olsun";
            urun.Fiyat = 5000;

            // Güncellenen ürünün veritabanında da güncellenmeini sağlayalım. 
            await context.SaveChangesAsync();
            #endregion
            #region ChangeTracker Nedir? 
            // ChangeTracker, context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır. 
            // Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler 
            // neticesinde update veya delete sorguları oluşturulur. 
            #endregion
            #region Takip Edilmeyen Nesneler Nasıl Güncellenir?
            ETicaretContext context6 = new();
            // Context tarafından gelen ürün değil. 
            Urun urun6 = new()
            {
                ID = 3,
                UrunAdi = "X urunu",
                Fiyat = 100
            };

            #region Update Fonksiyounu
            // ChangeTracker mekanizması ile takip edilmeyen nesnelerin güncellenebilmesi için 
            // Update fonksiyonu kullanılır. 
            // Update fonksiyonu kullanabilmek için ilgili nesenede kesinlikle Id değeri verilmelidir. 
            context.Urunler.Update(urun6);
            #endregion


            #endregion
            #region EntityState Nedir?

            // Bir entity instance'ının durumunu ifade eden bir referanstır. 
            Console.WriteLine(context.Entry(urun6).State);

            #endregion
            #region EF Core açısından bir verinin güncellenmesi gerektiği nasıl anlaşılıyor?
            Urun urun7 = await context.Urunler.FirstOrDefaultAsync(u => u.ID == 3);
            Console.WriteLine(context.Entry(urun7).State);

            urun7.UrunAdi = "Urun7";
            urun7.Fiyat = 3;
            Console.WriteLine(context.Entry(urun7).State);
            await context.SaveChangesAsync();
            Console.WriteLine(context.Entry(urun7).State);
            #endregion
            #region Birden fazla veri eklerken nelere dikkat edilmelidir?
            var urunler = await context.Urunler.ToListAsync();
            foreach (var item in urunler )
            {
                item.UrunAdi += "*";
            }
            context.SaveChanges();
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
