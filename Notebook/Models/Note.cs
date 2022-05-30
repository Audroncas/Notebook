using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [DisplayName("Label")]

        public int LabelId { get; set; }
        public Label Label { get; set; }
    }
}
