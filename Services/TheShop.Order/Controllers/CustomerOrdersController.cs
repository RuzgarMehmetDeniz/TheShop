using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheShop.Order.Dtos.CustomerOrderDtos;
using TheShop.Order.Services.CustomerOrderServices;

namespace TheShop.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrdersController(
            ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerOrders()
        {
            var values = await _customerOrderService.GetAllCustomerOrdersAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerOrderById(int id)
        {
            var value = await _customerOrderService.GetCustomerOrderByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetCustomerOrdersByUserId(
            string userId)
        {
            var values = await _customerOrderService.GetCustomerOrdersByUserIdAsync(userId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerOrder(CreateCustomerOrderDto createCustomerOrderDto)
        {
            await _customerOrderService.CreateCustomerOrderAsync(createCustomerOrderDto);
            return Ok("Sipariş başarıyla oluşturuldu.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerOrder(UpdateCustomerOrderDto updateCustomerOrderDto)
        {
            await _customerOrderService.UpdateCustomerOrderAsync(updateCustomerOrderDto);
            return Ok("Sipariş başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(int id)
        {
            await _customerOrderService.DeleteCustomerOrderAsync(id);
            return Ok("Sipariş başarıyla silindi.");
        }
    }
}
