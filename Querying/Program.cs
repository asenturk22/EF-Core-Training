using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Querying
{
    internal class Program
    {
        static async  Task Main(string[] args)
        {
            ETicaretContext context = new ETicaretContext();

            #region En Temel Basit Bir Sorgulama Nasıl Yapılır?
            #region Method Syntax
            var urunler = await context.Urunler.ToListAsync(); //Linq fonksiyonları
            #endregion
            #region @uery Syntax
            var urunler2 = await (from urun in context.Urunler //Linq sorguları
                           select urun).ToListAsync();
            #endregion
            #endregion

            #region Sorguyu Execute Etmek için Ne Yapmamız Gerekmektedir?
            #region ToListAsync
            #region Method Syntax
            urunler = await context.Urunler.ToListAsync();
            #endregion Query Syntax
            #region 
            urunler = await (from urun in context.Urunler
                             select urun).ToListAsync();
            #endregion
            #endregion

            int urunID = 5;
            string urunAdi = "2 nolu ürün";

            var urunler_2 = from urun in context.Urunler
                            where urun.ID > urunID && urun.UrunAdi.Contains(urunAdi)
                            select urun;
            #region Foreach

            urunID = 200;
            urunAdi = "3 nolu ürün";

            // IQueryable'ın execute edildiği nokta. 
            // burda yukarıda yapılan sorguda  id = 200 ve urunadi = "3 nolu ürün" olarak sorgu yapar. 
            // Bu duruma ertelenmiş çalışma' dan kaynaklanmaktadır. 
            foreach (var urun in urunler_2)
            {
                Console.WriteLine(urun.UrunAdi);
            }

            #region Deferred Execution(Ertelenmiş Çalışma)
            // IQueryable çalışmalarında ilgili kod yazıldığı noktada çalıştırılmaz. 
            // Çalıştırıldığı yani execute edildiği noktada tetiklenir. Bu duruma da 
            // ertelenmiş çalışma denir. 

            #endregion

            #endregion

            #endregion

            #region IQueryable ve IEnumerable Nedir? 

            urunler = await (from urun in context.Urunler
                      select urun).ToListAsync();

            #region IQueryable
            // EF Core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder. 
            #endregion
            #region IEnumerable
            // Sorgunun çalıştırılıp/execute edilip verilerin in memoriye yüklenmiş halini ifade eder. 
            #endregion 
            #endregion

            #region Çoğul Veri Getiren Sorgulama Fonksiyonları

            #endregion

            #region Tekil Veri Getiren Sorgulama Fonksiyonları 

            #endregion

            #region Diğer Sorgulama Fonksiyonları

            #endregion

            #region Sorgu Sonucu Dönüşüm Fonksiyonları

            #endregion

            #region GroupBy Fonksiyonu

            #endregion

            #region Foreach Fonksiyonu

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

        public ICollection<Parca> Parcalar { get; set; }
    }

    public class Parca
    {
        public int ID { get; set; }
        public string  ParcaAdi { get; set; }
    }
}
