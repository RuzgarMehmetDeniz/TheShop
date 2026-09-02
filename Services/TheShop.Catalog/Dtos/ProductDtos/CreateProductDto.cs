using System.ComponentModel.DataAnnotations;

namespace TheShop.Catalog.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ürün adı 2-100 karakter arasında olmalıdır.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Marka zorunludur.")]
        [StringLength(50, ErrorMessage = "Marka en fazla 50 karakter olabilir.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Kategori ID zorunludur.")]
        public string CategoryId { get; set; }
    }
}