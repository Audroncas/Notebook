using Notebook.Models;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Validation
{
    public class ExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = validationContext.GetService<NotebookContext>();
            var labels = context.Labels;
            if (labels.Any(l => l.Title == value))
            {
                return new ValidationResult("Label with this title already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
