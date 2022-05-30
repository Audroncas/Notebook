using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notebook.Models;
using Notebook.Services;

namespace Notebook.Pages.Notes
{
    public class CreateModel : PageModel
    {
        private readonly NotesRepository _repository;
        private readonly NotebookContext _context;
        private readonly string _userId;

        public SelectList LabelList { get; set; }

        [BindProperty]
        public Note Note { get; set; }

        public CreateModel(NotesRepository repository, NotebookContext context, UserService service)
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
                Note = _repository.GetNote(id);
                GenerateLabelsDropDownList(Note.LabelId);
            }
            else
            {
                //new
                GenerateLabelsDropDownList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Note.Title == null || Note.Content == null || Note.LabelId == 0)
            {
                GenerateLabelsDropDownList();
                return Page();
            }

            if (Note.Id == 0)
            {
                //create new note
                _repository.CreateNote(Note);
                return RedirectToPage("Index");
            }
            else
            {
                //edit note
                var noteFromDb = _repository.GetNote(Note.Id);
                noteFromDb.Title = Note.Title;
                noteFromDb.Content = Note.Content;
                noteFromDb.LabelId = Note.LabelId;
                _repository.UpdateNote(noteFromDb);
                return RedirectToPage("Index");
            }
        }

        public void GenerateLabelsDropDownList(object selectedLabel = null)
        {
            var labelsQuery = _context.Labels.Where(l => l.UserId == _userId).OrderBy(a => a.Title);
            LabelList = new SelectList(labelsQuery, "Id", "Title", selectedLabel);
        }
    }
}
