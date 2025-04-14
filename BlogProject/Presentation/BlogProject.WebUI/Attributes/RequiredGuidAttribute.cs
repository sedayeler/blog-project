using System.ComponentModel.DataAnnotations;

namespace BlogProject.WebUI.Attributes
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is Guid guid && guid != Guid.Empty;
        }
    }
}
