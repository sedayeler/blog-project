using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.WebUI.Models.Post
{
    public class UpdatePostViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Lütfen başlık giriniz.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Lütfen içerik giriniz.")]
        public string Content { get; set; }

        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public Guid CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
