using MediatR;

namespace BlogProject.Application.Features.Commands.Comment.DeleteComment
{
    public class DeleteCommentCommandRequest : IRequest<DeleteCommentCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
