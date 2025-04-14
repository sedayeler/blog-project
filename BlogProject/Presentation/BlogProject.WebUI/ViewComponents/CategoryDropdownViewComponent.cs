using BlogProject.Application.Features.Queries.Category.GetAllCategories;
using BlogProject.WebUI.Models.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.ViewComponents
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public CategoryDropdownViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQueryRequest());

            var model = categories.Select(c => new ListCategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View("_CategoryDropdown.cshtml", model);
        }
    }
}
