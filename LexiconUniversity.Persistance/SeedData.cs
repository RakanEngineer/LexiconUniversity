using Bogus;
using LexiconUniversity.Core.Entities;
using LexiconUniversity.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconUniversity.Persistance
{
    public class SeedData
    {

        private static Faker faker;

        public static async Task InitAsync(LexiconUniversityContext context)
        {
            if (await context.Students.AnyAsync()) return;

            faker = new Faker("sv");

            IEnumerable<Student> students = GenerateStudents(50);
            await context.AddRangeAsync(students);

            IEnumerable<Course> courses = GenerateCourses(20);
            await context.AddRangeAsync(courses);

            IEnumerable<Enrollment> enrollments = GenerateEnrollments(students, courses);
            await context.AddRangeAsync(enrollments);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Enrollment> GenerateEnrollments(IEnumerable<Student> students, IEnumerable<Course> courses)
        {
            Random rnd = new Random();

            List<Enrollment> enrollments = new List<Enrollment>();

            foreach (var student in students)
            {
                foreach (var course in courses)
                {
                    if (rnd.Next(0, 5) == 0)
                    {
                        Enrollment enrollment = new Enrollment
                        {
                            Course = course,
                            Student = student,
                            Grade = rnd.Next(1, 6)
                        };
                        enrollments.Add(enrollment);
                    }
                }
            }
            return enrollments;
        }

        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            List<Course> courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
                Course course = new Course
                {
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs())
                };
                courses.Add(course);
            }
            return courses;
        }

        private static IEnumerable<Student> GenerateStudents(int numberOfStudents)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                string avatar = faker.Internet.Avatar();
                string fName = faker.Name.FirstName();
                string lName = faker.Name.LastName();
                string email = faker.Internet.Email(fName, lName, "lexicon.se");

                Student student = new Student()
                {
                    Avatar = avatar,
                    FirstName = fName,
                    LastName = lName,
                    Email = email,
                    Address = new Address
                    {
                        Street = faker.Address.StreetName(),
                        City = faker.Address.City(),
                        ZipCode = faker.Address.ZipCode()
                    }

                };
                students.Add(student);
            }
            return students;
        }
    }

}
