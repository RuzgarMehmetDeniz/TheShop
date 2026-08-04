using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TheShop.Identity.Context;
using TheShop.Identity.Dtos;
using TheShop.Identity.Entities;

namespace TheShop.Identity.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(CreateRegisterDto createRegisterDto)
        {
            var user = new AppUser
            {
                Name = createRegisterDto.Name,
                Surname = createRegisterDto.SurName,
                Email = createRegisterDto.Email,
                UserName = createRegisterDto.UserName
            };

            var result = await _userManager.CreateAsync(user, createRegisterDto.Password);

            return result.Succeeded;
        }
        public async Task<string> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            var passwordResult = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            if (!passwordResult)
            {
                return "Başarısız";
            }

            return "Başarılı";
        }
        public async Task<List<ResultUsersDto>> GetAllUserAsync()
        {
            var users = await _userManager.Users.Select(user => new ResultUsersDto
                 {
                     Name = user.Name,
                     SurName = user.Surname,
                     Email = user.Email
                 })
                 .ToListAsync();

            return users;
        }
    }
}
