using TheShop.Discount.Dtos;

namespace TheShop.Discount.Services
{
    public interface IDiscountCodeService
    {
        Task<List<ResultDiscountCodeDto>> GetAllDiscountCodesAsync();

        Task<GetByIdDiscountCodeDto?> GetDiscountCodeByIdAsync(int id);

        Task CreateDiscountCodeAsync(CreateDiscountCodeDto createDiscountCodeDto);

        Task UpdateDiscountCodeAsync(UpdateDiscountCodeDto updateDiscountCodeDto);

        Task DeleteDiscountCodeAsync(int id);
    }
}
