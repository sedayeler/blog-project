using BlogProject.Application.Features.Commands.Post.CreatePost;
using BlogProject.Application.Features.Commands.Post.DeletePost;
using BlogProject.Application.Features.Commands.Post.UpdatePost;
using BlogProject.Application.Features.Queries.Category.GetAllCategories;
using BlogProject.Application.Features.Queries.Post.GetAllPosts;
using BlogProject.Application.Features.Queries.Post.GetByIdPost;
using BlogProject.WebUI.Models.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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

            List<ListPostViewModel> viewModel = response.Select(post => new ListPostViewModel
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                Title = post.Title,
                Content = post.Content.Length > 100 ? post.Content.Substring(0, 100) + "..." : post.Content,
                ImagePath = post.ImagePath,
                AuthorId = post.AuthorId,
                AuthorName = post.AuthorName,
                CategoryId = post.Category.Id,
                CategoryName = post.Category.Name
            }).ToList();

            return View(viewModel);
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

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _mediator.Send(new GetByIdPostRequest { Id = id });

            ListPostViewModel viewModel = new ListPostViewModel()
            {
                Id = response.Id,
                CreatedAt = response.CreatedAt,
                Title = response.Title,
                Content = response.Content,
                ImagePath = response.ImagePath,
                AuthorId = response.AuthorId,
                AuthorName = response.AuthorName,
                CategoryId = response.CategoryId,
                CategoryName = response.CategoryName
            };

            ViewBag.IsAuthor = false;

            if (User.Identity.IsAuthenticated)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (Guid.TryParse(userIdString, out var currentUserId))
                    ViewBag.IsAuthor = currentUserId == response.AuthorId;
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _mediator.Send(new GetByIdPostRequest { Id = id });

            UpdatePostViewModel model = new UpdatePostViewModel()
            {
                Id = response.Id,
                Title = response.Title,
                Content = response.Content,
                ImagePath = response.ImagePath,
                CategoryId = response.CategoryId,
            };

            ViewBag.Categories = new SelectList(await _mediator.Send(new GetAllCategoriesQueryRequest()), "Id", "Name", model.CategoryId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _mediator.Send(new GetAllCategoriesQueryRequest()), "Id", "Name", model.CategoryId);

                return View(model);
            }

            UpdatePostCommandRequest request = new UpdatePostCommandRequest()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                ImagePath = model.ImagePath,
                CategoryId = model.CategoryId
            };

            await _mediator.Send(request);

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePostCommandRequest() { Id = id });

            return RedirectToAction("Index", "Home");
        }
    }
}
