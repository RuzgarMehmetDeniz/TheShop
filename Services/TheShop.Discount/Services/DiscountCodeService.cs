using Dapper;
using TheShop.Discount.Context;
using TheShop.Discount.Dtos;

namespace TheShop.Discount.Services
{
    public class DiscountCodeService:IDiscountCodeService
    {
        private readonly DiscountContext _context;

        public DiscountCodeService(DiscountContext context)
        {
            _context = context;
        }

        public async Task<List<ResultDiscountCodeDto>> GetAllDiscountCodesAsync()
        {
            string query = "Select * From DiscountsCode";

            using var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultDiscountCodeDto>(query);

            return values.ToList();
        }

        public async Task<GetByIdDiscountCodeDto?> GetDiscountCodeByIdAsync(int id)
        {
            string query = "Select * From DiscountsCode Where DiscountCodeId=@discountCodeId";

            using var connection = _context.CreateConnection();

            var value = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCodeDto>(
                query,
                new
                {
                    discountCodeId = id
                });

            return value;
        }

        public async Task CreateDiscountCodeAsync(CreateDiscountCodeDto createDiscountCodeDto)
        {
            string query = @"Insert Into DiscountsCode
                            (DiscountCode,Rate,IsActive)
                            Values
                            (@DiscountCode,@Rate,@IsActive)";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, createDiscountCodeDto);
        }

        public async Task UpdateDiscountCodeAsync(UpdateDiscountCodeDto updateDiscountCodeDto)
        {
            string query = @"Update DiscountsCode
                             Set
                             DiscountCode=@DiscountCode,
                             Rate=@Rate,
                             IsActive=@IsActive
                             Where DiscountCodeId=@DiscountCodeId";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, updateDiscountCodeDto);
        }

        public async Task DeleteDiscountCodeAsync(int id)
        {
            string query = "Delete From DiscountsCode Where DiscountCodeId=@discountCodeId";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, new
            {
                discountCodeId = id
            });
        }
    }
}
