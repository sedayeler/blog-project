using MediatR;

namespace BlogProject.Application.Features.Commands.Comment.CreateComment
{
    public class CreateCommentCommandRequest : IRequest<CreateCommentCommandResponse>
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}
