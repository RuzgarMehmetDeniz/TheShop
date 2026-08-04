using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheShop.Identity.Dtos;
using TheShop.Identity.Services.UserServices;

namespace TheShop.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( CreateRegisterDto createRegisterDto)
        {
            var result = await _userService.CreateUserAsync( createRegisterDto );

            if (!result)
            {
                return BadRequest("Kullanıcı oluşturulamadı.");
            }

            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var result = await _userService.LoginAsync(userLoginDto);

            if (result == "Başarısız")
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [HttpGet("userlist")]
        public async Task<IActionResult> UserList()
        {
            var values = await _userService.GetAllUserAsync();
            return Ok(values);
        }
    }
}
