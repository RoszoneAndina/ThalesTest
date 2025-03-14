using System;

namespace ThalesDTO.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Slug { get; set; } = String.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = String.Empty;
        public CategoryDTO Category { get; set; } = new CategoryDTO();
        public string[] Images { get; set; } = [];
        public DateTime CreationAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public double Tax { get; set; }
    }

    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Slug { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public DateTime CreationAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
