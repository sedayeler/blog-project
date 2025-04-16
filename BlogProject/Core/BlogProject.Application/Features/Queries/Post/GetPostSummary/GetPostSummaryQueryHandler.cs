using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Queries.Post.SummarizePost
{
    public class GetPostSummaryQueryHandler : IRequestHandler<GetPostSummaryQueryRequest, GetPostSummaryQueryResponse>
    {
        private readonly IPostService _postService;

        public GetPostSummaryQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<GetPostSummaryQueryResponse> Handle(GetPostSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            string summary = await _postService.GetPostSummaryAsync(request.Id);

            return new GetPostSummaryQueryResponse()
            {
                Summary = summary
            };
        }
    }
}
