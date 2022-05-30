using Notebook.Validation;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Models
{
    public class Label
    {
        public int Id { get; set; }
        [Exists]
        [Required]
        public string Title { get; set; }
        public string Color { get; set; }
        public List<Note> Notes { get; set; }
        public string UserId { get; set; }
    }
}
