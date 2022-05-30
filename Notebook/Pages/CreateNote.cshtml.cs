using Microsoft.AspNetCore.Mvc;
using Notebook.Models;
using Notebook.Services;

namespace Notebook.Pages
{
    public class CreateNoteModel : LabelsListModel
    {
        private readonly NotesRepository _repository;
        private readonly NotebookContext _context;
        private readonly string _userId;

        [BindProperty]
        public Note Note { get; set; }

        public CreateNoteModel(NotesRepository repository, NotebookContext context, UserService service)
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
                GenerateLabelsDropDownList(_context, _userId, Note.LabelId);
            }
            else
            {
                //new
                GenerateLabelsDropDownList(_context, _userId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Note.Title == null || Note.Content == null || Note.LabelId == 0)
            {
                GenerateLabelsDropDownList(_context, _userId);
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
    }
}
