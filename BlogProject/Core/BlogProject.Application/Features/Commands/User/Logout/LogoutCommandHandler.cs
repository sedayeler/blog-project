using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Commands.User.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommandRequest, LogoutCommandResponse>
    {
        private readonly IUserService _userService;

        public LogoutCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LogoutCommandResponse> Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.LogoutAsync();

            return new LogoutCommandResponse();
        }
    }
}
