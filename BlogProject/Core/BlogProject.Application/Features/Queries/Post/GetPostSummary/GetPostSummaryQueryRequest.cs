using MediatR;

namespace BlogProject.Application.Features.Queries.Post.SummarizePost
{
    public class GetPostSummaryQueryRequest : IRequest<GetPostSummaryQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
