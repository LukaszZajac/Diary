using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Domains
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        //każda ocena jest przypisana do konkretnego ucznia
        //Dzięki tej właśc będziemy mieli dostęp do wszystkich pól tego studenta
        public Student Student { get; set; }
    }
}
