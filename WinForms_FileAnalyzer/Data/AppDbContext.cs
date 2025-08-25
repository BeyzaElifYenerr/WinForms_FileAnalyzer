using Microsoft.EntityFrameworkCore;
using WinForms_FileAnalyzer.Models;

namespace WinForms_FileAnalyzer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BEYZA\SQLEXPRESS;Database=FileAnalyzerDb;Trusted_Connection=True;");
        }
    }
}
