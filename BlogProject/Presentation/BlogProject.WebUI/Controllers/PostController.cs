using BlogProject.Application.Features.Commands.Post.CreatePost;
using BlogProject.Application.Features.Queries.Category.GetAllCategories;
using BlogProject.Application.Features.Queries.Post.GetAllPosts;
using BlogProject.WebUI.Models.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            List<GetAllPostsQueryResponse> response = await _mediator.Send(new GetAllPostsQueryRequest());
            return View(response);
        }

        private async Task PopulateCategoriesAsync(CreatePostViewModel viewModel)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQueryRequest());

            viewModel.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .OrderBy(s => s.Text)
                .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePostViewModel viewModel = new CreatePostViewModel();
            await PopulateCategoriesAsync(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync(viewModel);

                return View(viewModel);
            }

            CreatePostCommandRequest request = new CreatePostCommandRequest()
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                ImagePath = viewModel.ImagePath,
                CategoryId = viewModel.CategoryId
            };

            await _mediator.Send(request);

            return RedirectToAction("Index", "Home");
        }
    }
}
