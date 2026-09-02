using TheShop.Identity.Dtos;

namespace TheShop.Identity.Services.UserServices
{

    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateRegisterDto createRegisterDto);
        Task<string> LoginAsync(UserLoginDto userLoginDto);
        Task<List<ResultUsersDto>> GetAllUserAsync();
    }
}
