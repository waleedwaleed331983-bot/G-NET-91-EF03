using Assignment_03_EF_Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateOnly EnrollmentDate { get; set; }
        public int ProgressPercentage { get; set; }
        public EnrollmentStatus Status { get; set; }
        public int? FinalGrade { get; set; }
    }
}
