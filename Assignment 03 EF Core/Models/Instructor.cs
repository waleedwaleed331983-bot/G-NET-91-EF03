using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? Bio { get; set; }
        public DateOnly JoiningDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
