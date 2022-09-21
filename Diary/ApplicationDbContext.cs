using Diary.Models.Configurations;
using Diary.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        //wska¿emy klasy domenowe tak ¿eby EF wiedzia³ które to s¹ tabele domenowe i wiedzia³, jakie tabele stworzyæ
        public DbSet<Student>Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        //informujemy, ¿e powsta³o coœ takiego jak Konfiguracja (¿eby j¹ uwzglêdni³ EF przy tworzeniu nowej db)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }

    
}