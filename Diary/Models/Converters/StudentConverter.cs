using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Converters
{
    //konwertery są po to żeby można było w łatwy sposób przekonwertować domenową klasę Student do klasy StudentWrapper - wykorzystuję do tego metody rozszerzające
    public static class StudentConverter
    {
        //metody rozszerzające - rozszerzają jakiś typ onp dodatkową metodę

        //rozszerzamy klasę Student (w nawiasie this Student model) - zwracamy StudentWrapper
        public static StudentWrapper ToWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Activities = model.Activities,
                Group = new GroupWrapper 
                { 
                    Id = model.Group.Id, 
                    Name = model.Group.Name 
                },
                //lista ocen wyświetlana po przecinku - string.Join() -> metoda która zamienia listę na stringa;
                Math = String.Join(", ",model.Ratings
                .Where(y => y.SubjectId == (int)Subject.Math)
                .Select(y => y.Rate)),

                Physics = String.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.Physics)
                .Select(y => y.Rate)),

                PolishLang = String.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.PolishLang)
                .Select(y => y.Rate)),

                ForeignLang = String.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.ForeignLang)
                .Select(y => y.Rate)),

                Technology = String.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.Technology)
                .Select(y => y.Rate)),
            };
        }

        //konwersja w drugą stronę - rozszerzamy StudentWrapper
        //ToDao - Data Access Object
        public static Student ToDao(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                GroupId = model.Group.Id,
                Comments = model.Comments,
                Activities = model.Activities
            };
        }

        //metoda, która zwraca listę z ocenami - używana przy konwersji StudentWrapper do Student
        public static List<Rating>ToRatingDao(this StudentWrapper model)
        {
            var ratings = new List<Rating>();

            //konwertujemy stringa do listy ()

            //jeżeli we właściwości Math, która przechowuje oceny z matematyki po przecinku - nie jest pusta albo nie jest nullem to spróbujemy zamienić wszystkie te oceny na listę
            if (!string.IsNullOrWhiteSpace(model.Math))
            {
                //iterujemy się po każdym elemencie listy i dodaj za każdym razem nowy element do mojej listy
                model.Math.Split(',').ToList().ForEach(x => 
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Math
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.Physics))
            {
                //iterujemy się po każdym elemencie listy i dodaj za każdym razem nowy element do mojej listy
                model.Physics.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Physics
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.PolishLang))
            {
                //iterujemy się po każdym elemencie listy i dodaj za każdym razem nowy element do mojej listy
                model.PolishLang.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.PolishLang
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.ForeignLang))
            {
                //iterujemy się po każdym elemencie listy i dodaj za każdym razem nowy element do mojej listy
                model.ForeignLang.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.ForeignLang
                    }));
            }

            if (!string.IsNullOrWhiteSpace(model.Technology))
            {
                //iterujemy się po każdym elemencie listy i dodaj za każdym razem nowy element do mojej listy
                model.Technology.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Technology
                    }));
            }

            return ratings;
        }
    }
}
