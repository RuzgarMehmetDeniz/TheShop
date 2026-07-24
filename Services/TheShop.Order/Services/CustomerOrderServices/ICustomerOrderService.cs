using TheShop.Order.Dtos.CustomerOrderDtos;

namespace TheShop.Order.Services.CustomerOrderServices
{
    public interface ICustomerOrderService
    {
        Task<List<ResultCustomerOrderDto>> GetAllCustomerOrdersAsync();

        Task<GetByIdCustomerOrderDto> GetCustomerOrderByIdAsync(int customerOrderId);

        Task<List<ResultCustomerOrderDto>> GetCustomerOrdersByUserIdAsync(string userId);

        Task CreateCustomerOrderAsync(CreateCustomerOrderDto createCustomerOrderDto);

        Task UpdateCustomerOrderAsync(UpdateCustomerOrderDto updateCustomerOrderDto);

        Task DeleteCustomerOrderAsync(int customerOrderId);
    }
}
