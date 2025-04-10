using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Commands.User.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly IUserService _userService;

        public LoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(request.Email, request.Password);

            return new()
            {
                Succeeded = result
            };
        }
    }
}
