using Diary.Models.Domains;
using Diary.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diary.Models.Converters;
using Diary.Models;

namespace Diary
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper>GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                //Pobieramy wszystkich uczniów z ich grupami i ocenami
                //najpierw budujemy kwerendę
                //ale zapytanie nie jest od razu wywoływane; nic się jeszcze nie dzieje; od razu zostanie wywołane jak będzie ToList()
                var students = context.Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .AsQueryable();

                //jeśli jest różne od zera czyli jeśli chcemy wyświetlić uczniów z jakiejś konkretnej grupy to chcemy dokleić do tego zapytania, które też jeszcze nie jest wywoływane
                if (groupId != 0)
                {
                    students = students.Where(x => x.GroupId == groupId);
                }

                //teraz dopiero to zapytanie zostanie wygenerowane  i zostaną zwrócone wyniki - dzięki ToList() (interfejsowi IQeryable)
                //to zapytanie zostało niejako posklejane z powyższych zapytań i dopiero wywołane w jednym miejscu - return students.ToList()
                return students
                    .ToList()
                    .Select(x=>x.ToWrapper())//dla każdego ucznia wywołaj metodę ToWrapper()
                    .ToList();//i zwróć listę takich obiektów
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao(); //nowe oceny

            using (var context = new ApplicationDbContext())
            {
                //dane studenta
                UpdateStudentProperties(context,student);

                //oceny trzeba będzie porównać - czy zostały zmienione

                //pobieramy stare oceny tego konkretnego studenta (te co są w db)
                var studentsRatings = GetStudentsRatings(context, student);

                UpdateRate(student, ratings, context, studentsRatings, Subject.Math);
                UpdateRate(student, ratings, context, studentsRatings, Subject.Technology);
                UpdateRate(student, ratings, context, studentsRatings, Subject.Physics);
                UpdateRate(student, ratings, context, studentsRatings, Subject.ForeignLang);
                UpdateRate(student, ratings, context, studentsRatings, Subject.PolishLang);

                context.SaveChanges();
            }            
        }

        private static List<Rating>GetStudentsRatings(ApplicationDbContext context,Student student)
        {
            return context
                    .Ratings
                    .Where(x => x.StudentId == student.Id)
                    .ToList();
        }
        private void UpdateStudentProperties(ApplicationDbContext context, Student student)
        {
            var studentToUpdate = context.Students.Find(student.Id);
            studentToUpdate.Activities = student.Activities;
            studentToUpdate.Comments = student.Comments;
            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.GroupId = student.GroupId;
        }

        private static void UpdateRate(Student student, List<Rating>newRatings, ApplicationDbContext context,List<Rating>studentsRatings, Subject subject)
        {
            //z powyższej listy pobieramy oceny z matematyki tego studenta
            var subRatings = studentsRatings.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);

            //pobieramy nowe oceny
            var newSubRatings = newRatings.Where(x => x.SubjectId == (int)subject).Select(x => x.Rate);

            //porównanie nowych ocen ze starymi

            //oceny z db, które chcemy usunąć (za wyjątkiem tych nowych (lista intów)) z matematyki
            var subRatingsToDelete = subRatings.Except(newSubRatings).ToList();
            //są w nowych ocenach a nie było ich wcześniej
            var subRatingsToAdd = newSubRatings.Except(subRatings).ToList();

            //
            subRatingsToDelete.ForEach(x =>
            {
                var ratingToDelete = context.Ratings.First(y => y.Rate == x && y.StudentId == student.Id && y.SubjectId == (int)subject);
                context.Ratings.Remove(ratingToDelete);
            });

            subRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)subject,
                };
                context.Ratings.Add(ratingToAdd);
            });
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                //dla każdego obiektu w tej liście będziemy chcieli wykonać taką akcję:
                ratings.ForEach(x =>
                {
                    //przypisujemy dla tego obiektu Id Studenta
                    x.Id = dbStudent.Id;
                    //x.StudentId = dbStudent.Id;
                    //dodajemy ten rekord do kontekstu bazy danych
                    context.Ratings.Add(x);
                });
                context.SaveChanges(); //zapisujemy w bazie danych
            }
        }
    }
}
