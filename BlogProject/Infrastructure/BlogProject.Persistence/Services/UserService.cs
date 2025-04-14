using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs.User;
using BlogProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
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

            string subject = "Yorum Satırı'na Hoş Geldin! 🎉";
            string body = $@"Merhaba {user.FullName},<br/><br/>
                   Yorum Satırı ailesine katıldığın için çok mutluyuz! <br/>
                   Artık kendi yazılarını paylaşabilir, başkalarının yazılarına yorum yapabilir ve bu güzel topluluğun bir parçası olabilirsin.<br/><br/>
                   Ne duruyorsun? İlk yazını hemen oluştur! 👇<br/>
                   <a href='https://localhost:7087/post/create' >✍️ Yazımı Paylaş</a><br/><br/>

                   Her fikrin değerli, her yorumun önemli.<br/>
                   Keyifli paylaşımlar!<br/><br/>

                   Sevgiler,<br/>
                   <b>Yorum Satırı Ekibi 💜</b>";

            await _mailService.SendMailAsync(user.Email, subject, body);

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
