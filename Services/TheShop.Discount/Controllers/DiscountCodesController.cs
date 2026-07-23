using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheShop.Discount.Dtos;
using TheShop.Discount.Services;

namespace TheShop.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCodesController : ControllerBase
    {
        private readonly IDiscountCodeService _discountCodeService;

        public DiscountCodesController(IDiscountCodeService discountCodeService)
        {
            _discountCodeService = discountCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountCodeList()
        {
            var values = await _discountCodeService.GetAllDiscountCodesAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCode(int id)
        {
            var value = await _discountCodeService.GetDiscountCodeByIdAsync(id);

            if (value == null)
                return NotFound();

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCode(CreateDiscountCodeDto createDiscountCodeDto)
        {
            await _discountCodeService.CreateDiscountCodeAsync(createDiscountCodeDto);
            return Ok("İndirim kodu başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCode(UpdateDiscountCodeDto updateDiscountCodeDto)
        {
            await _discountCodeService.UpdateDiscountCodeAsync(updateDiscountCodeDto);
            return Ok("İndirim kodu başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountCode(int id)
        {
            await _discountCodeService.DeleteDiscountCodeAsync(id);
            return Ok("İndirim kodu başarıyla silindi.");
        }
    }
}
