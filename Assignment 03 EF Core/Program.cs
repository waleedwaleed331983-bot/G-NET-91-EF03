using Assignment_03_EF_Core.Contexts;
using Assignment_03_EF_Core.Enums;
using Assignment_03_EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_03_EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new MyContextClass();

            #region Part 02 - Crud Operations

            #region Create Operation
            // Create a new instructor
            var firstInstructor = new Instructor()
            {
                FullName = "John Doe",
                EmailAddress = "john.doe@example.com",
                Bio = "An experienced instructor in computer science.",
                JoiningDate = new DateOnly(2020, 1, 15)
            };

            dbContext.Add(firstInstructor);
            dbContext.SaveChanges();
            Console.WriteLine($"Generated ID for this Instructor is: {firstInstructor.Id}");
            Console.WriteLine("============================");

            /////////////////////////////////////////////////////////

            // Create a new Category

            var firstCategory = new Category()
            {
                Name = "Programming",
                Description = "Courses related to programming languages and software development."
            };

            dbContext.Add(firstCategory);
            dbContext.SaveChanges();
            Console.WriteLine($"Generated ID for this Category is: {firstCategory.Id}");
            Console.WriteLine("============================");
            ////////////////////////////////////////////////////////

            // Create a new Course

            var firstCourse = new Course()
            {
                Title = "Introduction to C#",
                Description = "Learn the basics of C# programming language.",
                Price = 199.99m,
                Level = CourseLevel.Beginner,
                IsPublished = true,
                InstructorId = firstInstructor.Id,
                CategoryId = firstCategory.Id
            };
            dbContext.Add(firstCourse);
            dbContext.SaveChanges();
            Console.WriteLine($"Generated ID for this Course is: {firstCourse.Id}");

            /////////////////////////////////////////////////////////

            // Create Several Students

            var firstStudent = new Student()
            {
                FullName = "Alice Smith",
                EmailAddress = "alice@example.com",
                DateOfBirth = new DateOnly(2000, 1, 4)
            };

            var secondStudent = new Student()
            {
                FullName = "Timi Charlton",
                EmailAddress = "timi@example.com",
                DateOfBirth = new DateOnly(2005, 1, 4)
            };

            var thirdStudent = new Student()
            {
                FullName = "Joao Mora",
                EmailAddress = "joao@example.com",
                DateOfBirth = new DateOnly(1999, 1, 4)
            };

            dbContext.AddRange(firstStudent, secondStudent, thirdStudent);
            dbContext.SaveChanges();
            Console.WriteLine("============================");
            Console.WriteLine($"Generated Id for First Student is: {firstStudent.Id}");
            Console.WriteLine("============================");
            Console.WriteLine($"Generated Id for Second Student is: {secondStudent.Id}");
            Console.WriteLine("============================");
            Console.WriteLine($"Generated Id for Third Student is: {thirdStudent.Id}");
            ///////////////////////////////////////////////////////

            // Create a new Enrollment

            int studentId = 1;
            int courseId = 60;

            var studentSelected = dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            var courseSelected = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);

            if (studentSelected == null)
            {
                Console.WriteLine($"Refused: student {studentId} does not exist.");
            }
            else if (courseSelected == null)
            {
                Console.WriteLine($"Refused: course {courseId} does not exist.");
            }
            else if (!courseSelected.IsPublished)
            {
                Console.WriteLine($"Refused: course '{courseSelected.Title}' is not published.");
            }
            else if (dbContext.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
            {
                Console.WriteLine($"Refused: {studentSelected.FullName} is already enrolled in '{courseSelected.Title}'.");
            }
            else
            {
                var enrollment = new Enrollment
                {
                    StudentId = studentSelected.Id,
                    CourseId = courseSelected.Id,
                    EnrollmentDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = EnrollmentStatus.Active,
                    ProgressPercentage = 0
                };

                dbContext.Enrollments.Add(enrollment);
                dbContext.SaveChanges();

                Console.WriteLine($"{studentSelected.FullName} was enrolled in '{courseSelected.Title}'.");
            }

            //
            #endregion

            #region Read Operation

            var course = dbContext.Courses.Find(5);
            if (course != null)
            {
                Console.WriteLine($"Course found: {course.Title}");
            }
            /////////////////////////////////////////////////////


            var course02 = dbContext.Courses.FirstOrDefault(c => c.Title == "Introduction to C#");
            if (course02 != null)
            {
                Console.WriteLine("===========================");
                Console.WriteLine($"Course found: {course02.Title}");
            }
            else
            {
                Console.WriteLine("===========================");
                Console.WriteLine("Course not found.");
            }
            ////////////////////////////////////////////////////////

            var res = dbContext.Courses.Where(c => c.Price > 100 && c.Price < 250).OrderByDescending(c => c.Price).Select(c => new { c.Title, c.Level, c.Price });
            Console.WriteLine("===========================");

            foreach (var item in res)
            {
                Console.WriteLine($"Title: {item.Title}, Level: {item.Level}, Price: {item.Price}");
            }
            ///////////////////////////////////////////////////////

            Console.WriteLine("===========================");

            int courseId02 = 5;
            foreach (var item in dbContext.Courses.ToList())
            {
                var ids = dbContext.Enrollments
                    .Where(e => e.CourseId == item.Id)
                    .Select(e => e.StudentId)
                    .Distinct()
                    .ToList();

                var students = dbContext.Students
                    .Where(s => ids.Contains(s.Id))
                    .ToList();

                Console.WriteLine($"Course {item.Id}: {students.Count} student(s)");
                foreach (var s in students)
                    Console.WriteLine($"{s.FullName}");
            }
            Console.WriteLine("===========================");
            ////////////////////////////////////////////////////


            int categoryId = 1;

            var category = dbContext.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine($"Category {categoryId} does not exist.");
            }
            else
            {
                var subCategories = dbContext.Categories
                    .Where(c => c.ParentCategoryId == categoryId)
                    .ToList();

                Console.WriteLine($"Direct sub-categories of '{category.Name}' ({subCategories.Count}):");
                foreach (var sub in subCategories)
                {
                    Console.WriteLine($"  {sub.Id}: {sub.Name}");
                }
            }

            var topLevel = dbContext.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine($"Top-level categories ({topLevel.Count}):");
            foreach (var top in topLevel)
            {
                Console.WriteLine($"  {top.Id}: {top.Name}");
            }



            #endregion

            #region Update Operation

            var result = dbContext.Courses.FirstOrDefault(c => c.Id == 5);
            if (result != null)
            {
                result.Price = 249.99m;
                dbContext.SaveChanges();
                Console.WriteLine("===========================");
                Console.WriteLine($"Price is updated to: {result.Price}");
            }
            else
            {
                Console.WriteLine("===========================");
                Console.WriteLine("Course not found for update.");
            }
            ////////////////////////////////////////////////////////




            int studentIdForUpdate = 1;
            int courseIdForUpdate = 60;
            int newProgress = 80;

            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine($"This is an invalid progress value. Please enter a value between 0 and 100.");
                return;
            }

            var enrollmentToUpdate = dbContext.Enrollments.Find(studentIdForUpdate, courseIdForUpdate);

            if (enrollmentToUpdate == null)
            {
                Console.WriteLine("This is an invalid enrollment. Please check the student and course IDs.");
                return;
            }

            enrollmentToUpdate.ProgressPercentage = newProgress;

            if (newProgress == 100)
            {
                enrollmentToUpdate.Status = EnrollmentStatus.Completed;
            }

            dbContext.SaveChanges();
            Console.WriteLine($"Progress updated to {newProgress}.");

            ///////////////////////////////////////////////////////////

            var coursesToPublish = dbContext.Courses
                    .Where(c => c.IsPublished == false)
                    .ToList();

            foreach (var item in coursesToPublish)
            {
                course.IsPublished = true;
                course.PublishedDate = DateOnly.FromDateTime(DateTime.Now);
            }

            dbContext.SaveChanges();

            Console.WriteLine($"The following courses have been published:{coursesToPublish.Count}");

            #endregion


            #region Delete Operation


            var courseIdForDelete = 56;
            var studentIdForDelete = 1;

            var enrollmentForDelete = dbContext.Enrollments.Find(studentIdForDelete, courseIdForDelete);

            if (enrollmentForDelete == null)
            {
                Console.WriteLine("The Enrollment is not found.");
            }
            else
            {
                dbContext.Enrollments.Remove(enrollmentForDelete);
                dbContext.SaveChanges();

                Console.WriteLine("The Enrollment has been deleted.");
            }
            /////////////////////////////////////////////////////////////



            int categoryIdForDelete = 1;

            try
            {
                var categoryByFirstWay = dbContext.Categories.Find(categoryIdForDelete);

                if (categoryByFirstWay != null)
                {
                    dbContext.Categories.Remove(categoryByFirstWay);
                    dbContext.SaveChanges();
                    Console.WriteLine("Category deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Details: {ex.InnerException?.Message}");
            }
            //
            int categoryIdForSafeDelete = 1;

            var categoryForSafeDelete = dbContext.Categories.Find(categoryIdForSafeDelete);

            if (categoryForSafeDelete == null)
            {
                Console.WriteLine($"Refused: category {categoryIdForSafeDelete} does not exist.");
            }
            else if (dbContext.Categories.Any(c => c.ParentCategoryId == categoryIdForSafeDelete))
            {
                Console.WriteLine($"Refused: category '{category.Name}' still has sub-categories.");
            }
            else if (dbContext.Courses.Any(c => c.CategoryId == categoryIdForSafeDelete))
            {
                Console.WriteLine($"Refused: category '{category.Name}' still has courses.");
            }
            else
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();

                Console.WriteLine($"Category '{category.Name}' was deleted.");
            }

            /////////////////////////////////////////////////////////////

            var res06 =dbContext.Courses.Where(c => c.Enrollments.Count > 0).ToList();

            if (res06.Count == 0 || res06 == null)
            {
                Console.WriteLine("This is an invalid course. Please check the course ID.");
            }

            else
            {
                Console.WriteLine($" (Before Deletion) Found {res06.Count} courses with enrollments.");
                dbContext.Courses.RemoveRange(res06);
                dbContext.SaveChanges();
                Console.WriteLine("The following courses have been deleted:");
                foreach (var item in res06)
                {
                    Console.WriteLine($" - {item.Title}");
                }
                Console.WriteLine($" (After Deletion) Found {res06.Count} courses with enrollments.");
            }
            ///////////////////////////////////////////////////////////
            
            
            
            
            
            #endregion

            #endregion

            Console.ReadKey();
        }
    }
}
