using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs.User;
using BlogProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new RegisterResponse()
                {
                    Succeeded = false,
                    Message = "Bu e-posta adresiyle bir kullanıcı zaten kayıtlı."
                };
            }

            User user = new User()
            {
                FullName = request.FullName,
                UserName = request.Username,
                Email = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            RegisterResponse response = new RegisterResponse() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kayıt işlemi başarıyla tamamlandı.";
            else
                response.Message = string.Join(" ", result.Errors.Select(e => e.Description));
            //response.Message = string.Join("<br>", result.Errors.Select(e => e.Description));

            return response;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("E-posta ya da şifre hatalı.");

            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
