using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PercobaanAPI7.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Additional model configuration if needed
        }
    }

    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Saldo { get; set; }
        public int Hutang { get; set; }
    }
}
