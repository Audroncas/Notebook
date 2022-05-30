using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notebook.Models;
using Notebook.Repositories;
using Notebook.Services;

namespace Notebook.Pages.Labels
{
    public class CreateModel : PageModel
    {
        private readonly LabelsRepository _repository;
        private readonly NotebookContext _context;
        private readonly string _userId;

        [BindProperty]
        public Label Label { get; set; }

        public CreateModel(LabelsRepository repository, NotebookContext context, UserService service)
        {
            _repository = repository;
            _context = context;
            _userId = service.GetUserId();
        }

        public IActionResult OnGet(int id)
        {
            if (id != 0)
            {
                //edit
                Label = _repository.GetLabel(id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Label.Id == 0)
            {
                //create new label
                if (Label.Title == null || _repository.GetLabels(_userId).Any(l => l.Title == Label.Title))
                {
                    return Page();
                }
                Label.UserId = _userId;
                _repository.CreateLabel(Label);
            }
            else
            {
                //edit label
                if (Label.Title == null)
                {
                    return Page();
                }
                var labelFromDb = _repository.GetLabel(Label.Id);
                labelFromDb.Title = Label.Title;
                labelFromDb.Color = Label.Color;
                _repository.UpdateLabel(labelFromDb);
            }
            return RedirectToPage("Index");

        }
    }
}
