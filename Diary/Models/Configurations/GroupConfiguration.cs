using Diary.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Configurations
{
    public class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            //Tworzenie pod konkretną nazwą tabeli w db
            ToTable("dbo.Groups");

            //Niech właściwość Id w tabeli Groups nie było inkrementowane o 1 dla każdego nowego rekordu
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
