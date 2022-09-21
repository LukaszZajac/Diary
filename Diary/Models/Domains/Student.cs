using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Domains
{
    public class Student
    {
        public Student()
        {
            Ratings = new Collection<Rating>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }

        public bool Activities { get; set; }
        public int GroupId { get; set; }

        //Student będzie przypisany do grupy - w klasie Student mamy dostęp do jednej przypisanej grupy
        //Informacje o tej grupie będą przechowywane w tej właściwości dzięki czemu później przy pisaniu zapytań w EF to będzie można się odwołać do tej grupy 
        
        public Group Group { get; set; }

        //każdy student może mieć wiele ocen
        public ICollection<Rating> Ratings { get; set; }
    }
}
