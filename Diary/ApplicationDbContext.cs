using Diary.Models.Configurations;
using Diary.Models.Domains;
using Diary.Properties;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        private static string _serverAddress = Settings.Default.ServerAddress;
        private static string _serverName = Settings.Default.ServerName;
        private static string _databaseName = Settings.Default.Database;
        private static string _user = Settings.Default.User;
        private static string _password = Settings.Default.Password;

        private static string _connectionString = $@"Server={_serverAddress}\{_serverName};Database={_databaseName};User Id={_user};Password={_password};";

        public ApplicationDbContext()
            : base(_connectionString)
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