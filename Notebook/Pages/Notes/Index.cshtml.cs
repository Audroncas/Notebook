using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notebook.Models;
using Notebook.Services;

namespace Notebook.Pages.Notes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly NotebookContext _context;
        private readonly string _userId;

        public List<Note> Notes;
        public SelectList LabelTitles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchLabel { get; set; }

        public IndexModel(NotesRepository repository, UserService service, NotebookContext context)
        {
            _repository = repository;
            _context = context;
            _userId = service.GetUserId();
        }

        public void OnGet()
        {
            var labelsQuery = _context.Labels.Where(l => l.UserId == _userId).Select(l => l.Title).OrderBy(l => l);
            LabelTitles = new SelectList(labelsQuery);

            Notes = _repository.GetNotes(_userId, SearchInput, SearchLabel);
        }

        public IActionResult OnPostDelete(int id)
        {
            var note = _repository.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }

            _repository.DeleteNote(id);
            return RedirectToPage("Index");
        }
    }
}
