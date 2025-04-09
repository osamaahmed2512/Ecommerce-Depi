using System.ComponentModel.DataAnnotations;

namespace eCommerce_dpei.Models
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ParentId must be a positive integer if provided")]
        public int? ParentId { get; set; }
    }
}