using MediatR;

namespace BlogProject.Application.Features.Commands.User.Register
{
    public class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
