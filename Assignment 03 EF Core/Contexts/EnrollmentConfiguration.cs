using Assignment_03_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Contexts
{
    internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        void IEntityTypeConfiguration<Enrollment>.Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId });
            builder.Property(e => e.StudentId).IsRequired();
            builder.Property(e => e.CourseId).IsRequired(); 

            builder.Property(e => e.EnrollmentDate).IsRequired();

            builder.Property(e => e.Status).IsRequired();

            builder.HasOne(e => e.Student).
                WithMany(e => e.Enrollments)
                .HasForeignKey(e => e.StudentId);
            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
            builder.ToTable(t => t.HasCheckConstraint(
             "CK_Enrollment_Progress",
             "[ProgressPercentage] BETWEEN 0 AND 100"));

        }
    }
}
