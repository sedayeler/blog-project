using MediatR;

namespace BlogProject.Application.Features.Commands.Post.SummarizePost
{
    public class SummarizePostCommandRequest : IRequest<SummarizePostCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
