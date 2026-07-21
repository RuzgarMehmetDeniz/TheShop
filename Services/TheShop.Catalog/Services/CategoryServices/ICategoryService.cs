using TheShop.Catalog.Dtos.CategoryDtos;

namespace TheShop.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();

        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);

        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);

        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);

        Task DeleteCategoryAsync(string id);
    }
}
