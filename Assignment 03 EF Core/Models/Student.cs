using Assignment_03_EF_Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }

}
