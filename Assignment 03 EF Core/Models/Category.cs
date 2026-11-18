using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_03_EF_Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = new HashSet<Category>();

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
