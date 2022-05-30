using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notebook.Models;
using Notebook.Repositories;
using Notebook.Services;

namespace Notebook.Pages.Labels
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly LabelsRepository _repository;
        private readonly UserService _service;

        public List<Label> Labels;

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        public IndexModel(LabelsRepository repository, UserService service)
        {
            _repository = repository;
            _service = service;
        }

        public void OnGet()
        {
            var userId = _service.GetUserId();
            Labels = _repository.GetLabels(userId, SearchInput);
        }

        public IActionResult OnPostDelete(int id)
        {
            var label = _repository.GetLabel(id);
            if (label == null)
            {
                return NotFound();
            }

            _repository.DeleteLabel(id);
            return RedirectToPage("Index");
        }
    }
}
