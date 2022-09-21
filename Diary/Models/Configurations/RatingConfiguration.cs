using Diary.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Configurations
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration()
        {
            //Tworzenie pod konkretną nazwą tabeli w db
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);
        }
    }
}
