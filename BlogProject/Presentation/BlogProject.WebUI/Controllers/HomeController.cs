using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogProject.WebUI.Models;
using BlogProject.Application.Features.Queries.Post.GetAllPosts;
using BlogProject.WebUI.Models.Post;
using MediatR;

namespace BlogProject.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        List<GetAllPostsQueryResponse> response = await _mediator.Send(new GetAllPostsQueryRequest());

        List<ListPostViewModel> viewModel = response
            .OrderByDescending(post => post.CreatedAt)
            .Select(post => new ListPostViewModel
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                Title = post.Title,
                Content = post.Content.Length > 200 ? post.Content.Substring(0, 200) + "..." : post.Content,
                ImagePath = post.ImagePath,
                AuthorId = post.UserId,
                AuthorName = post.UserFullName,
                CategoryId = post.CategoryId,
                CategoryName = post.CategoryName
            }).ToList();

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
