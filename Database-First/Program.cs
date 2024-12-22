namespace Database_First
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}


/*
 PMC komut aracılığıyla . 

PMC> Scaffold-DbContext 
       "Server=localhost\SQLEXPRESS;Database=NORTHWND;Integrated Security=True;" 
       Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
 */