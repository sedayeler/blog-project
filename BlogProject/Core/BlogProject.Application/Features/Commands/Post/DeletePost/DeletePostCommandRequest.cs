using MediatR;

namespace BlogProject.Application.Features.Commands.Post.DeletePost
{
    public class DeletePostCommandRequest : IRequest<DeletePostCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
