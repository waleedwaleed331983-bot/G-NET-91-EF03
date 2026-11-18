using Assignment_03_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Contexts
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        void IEntityTypeConfiguration<Student>.Configure(EntityTypeBuilder<Student> builder)
        {
            
        }
    }
}
