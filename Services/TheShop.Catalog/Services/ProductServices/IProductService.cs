using TheShop.Catalog.Dtos.ProductDtos;

namespace TheShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();

        Task<GetByIdProductDto> GetByIdProductAsync(string id);

        Task CreateProductAsync(CreateProductDto createProductDto);

        Task UpdateProductAsync(UpdateProductDto updateProductDto);

        Task DeleteProductAsync(string id);
    }
}
