using System.ComponentModel.DataAnnotations;

namespace TheShop.Catalog.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Kategori adı 2-50 karakter arasında olmalıdır.")]
        public string CategoryName { get; set; }
    }
}