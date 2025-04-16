using System.ComponentModel.DataAnnotations;

namespace BlogProject.WebUI.Models.Comment
{
    public class CreateCommentViewModel
    {
        [Required(ErrorMessage = "Lütfen bir yorum yazınız.")]
        [MaxLength(500, ErrorMessage = "Yorumunuz en fazla 500 karakter olabilir.")]
        public string Content { get; set; }

        [Required]
        public Guid PostId { get; set; }
    }
}
