using Assignment_03_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Contexts
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {



        void IEntityTypeConfiguration<Course>.Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Id).UseIdentityColumn().IsRequired();

            builder.Property(c => c.Title).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(150);

            builder.Property(c => c.Price).HasPrecision(8, 2).IsRequired();

            builder.Property(c => c.Level).IsRequired();

            builder.Property(c => c.IsPublished).IsRequired();

            builder.HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId);

            builder.HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId);
        }
    }
}
