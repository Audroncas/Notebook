using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notebook.Models;

namespace Notebook.Pages
{
    public class LabelsListModel : PageModel
    {
        public SelectList LabelTitles { get; set; }

        
    }
}
