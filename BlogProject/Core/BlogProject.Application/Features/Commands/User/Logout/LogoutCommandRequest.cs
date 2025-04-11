using MediatR;

namespace BlogProject.Application.Features.Commands.User.Logout
{
    public class LogoutCommandRequest : IRequest<LogoutCommandResponse>
    {
    }
}
