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

        //wska�emy klasy domenowe tak �eby EF wiedzia� kt�re to s� tabele domenowe i wiedzia�, jakie tabele stworzy�
        public DbSet<Student>Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        //informujemy, �e powsta�o co� takiego jak Konfiguracja (�eby j� uwzgl�dni� EF przy tworzeniu nowej db)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }

    
}