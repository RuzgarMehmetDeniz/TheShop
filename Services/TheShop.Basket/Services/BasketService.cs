using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TheShop.Basket.Dtos;

namespace TheShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IDistributedCache _distributedCache;

        public BasketService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        private static string GetBasketKey(string userId)
        {
            return $"basket:{userId}";
        }
        public async Task DeleteBasketAsync(string userId)
        {
           var key = GetBasketKey(userId);
            await _distributedCache.RemoveAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string userId)
        {
           var key = GetBasketKey(userId);
            var basketJson = await _distributedCache.GetStringAsync(key);
            if(string.IsNullOrWhiteSpace(basketJson))
            {
                return null;
            }
            return JsonSerializer.Deserialize<BasketDto>(basketJson);
        }

        public async Task SaveBasketAsync(BasketDto basketDto)
        {
            var key = GetBasketKey(basketDto.UserId);
            var basketJson = JsonSerializer.Serialize(basketDto);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            };
            await _distributedCache.SetStringAsync( key , basketJson, cacheOptions);
        }
    }
}
