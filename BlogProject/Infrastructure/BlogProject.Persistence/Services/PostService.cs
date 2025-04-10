using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs;
using BlogProject.Application.Repositories;
using BlogProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Persistence.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly UserManager<User> _userManager;

        public PostService(IHttpContextAccessor httpContextAccessor, IPostReadRepository postReadRepository, IPostWriteRepository postWriteRepository, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
            _userManager = userManager;
        }

        public async Task CreatePostAsync(CreatePostDto dto)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Kullanıcı doğrulama hatası.");

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            Post post = new Post()
            {
                Title = dto.Title,
                Content = dto.Content,
                ImagePath = dto.ImagePath,
                UserId = user.Id,
                CategoryId = dto.CategoryId
            };

            await _postWriteRepository.AddAsync(post);
            await _postWriteRepository.SaveAsync();
        }

        public async Task UpdatePostAsync(UpdatePostDto dto)
        {
            Post post = await _postReadRepository.GetByIdAsync(dto.Id);
            if (post == null)
                throw new Exception("Post not found.");

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.ImagePath = dto.ImagePath;
            post.CategoryId = dto.CategoryId;

            _postWriteRepository.Update(post);
            await _postWriteRepository.SaveAsync();
        }

        public async Task DeletePostAsync(Guid id)
        {
            Post post = await _postReadRepository.GetByIdAsync(id);
            if (post == null)
                throw new Exception("Post not found.");

            _postWriteRepository.Remove(post);
            await _postWriteRepository.SaveAsync();
        }

        public async Task<List<ListPostDto>> GetAllPostsAsync()
        {
            List<Post> posts = await _postReadRepository.GetAll()
                .Include(p => p.User)
                .Include(p => p.Category)
                .ToListAsync();

            List<ListPostDto> postDtos = posts.Select(p => new ListPostDto
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content != null && p.Content.Length > 100
                    ? p.Content.Substring(0, 100) + "..."
                    : p.Content,
                ImagePath = p.ImagePath,
                AuthorName = p.User.FullName,
                CategoryName = p.Category.Name
            }).ToList();

            return postDtos;
        }

        public async Task<ListPostDto> GetByIdPostAsync(Guid id)
        {
            Post post = await _postReadRepository.GetByIdAsync(id);
            if (post == null)
                throw new Exception("Post not found.");

            ListPostDto postDto = new ListPostDto()
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                Title = post.Title,
                Content = post.Content,
                ImagePath = post.ImagePath,
                AuthorName = post.User.FullName,
                CategoryName = post.Category.Name,
            };

            return postDto;
        }
    }
}