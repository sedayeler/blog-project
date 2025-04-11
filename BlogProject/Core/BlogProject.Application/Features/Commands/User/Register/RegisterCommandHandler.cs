using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs.User;
using MediatR;

namespace BlogProject.Application.Features.Commands.User.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly IUserService _userService;

        public RegisterCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            RegisterResponse response = await _userService.RegisterAsync(new()
            {
                FullName = request.FullName,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            });

            return new RegisterCommandResponse()
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
        }
    }
}
