using BlogProject.Application.Features.Commands.User.Login;
using BlogProject.Application.Features.Commands.User.Logout;
using BlogProject.Application.Features.Commands.User.Register;
using BlogProject.WebUI.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            RegisterCommandRequest request = new RegisterCommandRequest
            {
                FullName = model.FullName,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            RegisterCommandResponse response = await _mediator.Send(request);

            if (response.Succeeded)
            {
                TempData["Success"] = response.Message;
                return RedirectToAction("Login", "User");
            }

            ModelState.AddModelError("", response.Message);
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            LoginCommandRequest request = new LoginCommandRequest()
            {
                Email = model.Email,
                Password = model.Password
            };

            LoginCommandResponse response = await _mediator.Send(request);

            if (response.Succeeded)
            {
                TempData["Success"] = "Giriş başarılı..";
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "E-posta ya da şifre hatalı.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommandRequest());

            return RedirectToAction("Index", "Home");
        }
    }
}
