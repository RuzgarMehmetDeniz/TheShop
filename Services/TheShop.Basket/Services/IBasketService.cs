using TheShop.Basket.Dtos;

namespace TheShop.Basket.Services
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string userId);

        Task SaveBasketAsync(BasketDto basketDto);

        Task DeleteBasketAsync(string userId);
    }
}
