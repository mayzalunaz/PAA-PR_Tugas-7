//using Microsoft.EntityFrameworkCore;

//namespace PercobaanAPI7.Models
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public DbSet<Person> People { get; set; }
//        public DbSet<Detail> Details { get; set; }

//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Person>()
//                .HasOne(p => p.Detail)
//                .WithOne(d => d.Person)
//                .HasForeignKey<Detail>(d => d.Id);
//        }
//    }
//}
