using Assignment_03_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Contexts
{
    internal class InstructorConfiguration: IEntityTypeConfiguration<Instructor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Instructor> builder)
        {
            builder.Property(i => i.Id).IsRequired().UseIdentityColumn();

            builder.Property(i => i.FullName).IsRequired().HasMaxLength(100);

            builder.Property(i => i.EmailAddress).IsRequired().HasMaxLength(100);

            builder.Property(i => i.Bio).HasMaxLength(75);

            builder.Property(i => i.JoiningDate).IsRequired();

        }
    
    }
}
