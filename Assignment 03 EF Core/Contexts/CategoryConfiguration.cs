using Assignment_03_EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Contexts
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        void IEntityTypeConfiguration<Category>.Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Id).UseIdentityColumn().IsRequired();
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(150);

            builder.HasOne(c => c.ParentCategory)
               .WithMany(c => c.SubCategories)
               .HasForeignKey(c => c.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
