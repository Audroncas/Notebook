using Microsoft.EntityFrameworkCore;
using Notebook.Models;

namespace Notebook.Pages
{
    public class NotesRepository
    {
        private readonly NotebookContext _context;

        public NotesRepository(NotebookContext context)
        {
            _context = context;
        }

        public void CreateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException();
            }
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public List<Note> GetNotes(string userId, string noteTitle = null, string labelTitle = null)
        {
            var notes = _context.Notes.Include(n=>n.Label).Where(n => n.Label.UserId == userId).ToList();

            if (!string.IsNullOrEmpty(noteTitle))
            {
                notes = notes.Where(n => n.Title.Contains(noteTitle, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(labelTitle))
            {
                notes = notes.Where(n => n.Label.Title == labelTitle).ToList();
            }
            return notes.OrderBy(n => n.Id).ToList(); ;
        }

        public Note GetNote(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public void DeleteNote(int id)
        {
            var note = GetNote(id);
            if (note == null)
            {
                throw new ArgumentNullException($"Note with id {id} was not found!");
            }
            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

        public void UpdateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException();
            }

            var noteToUpdate = GetNote(note.Id);
            if (noteToUpdate == null)
            {
                throw new ArgumentNullException();
            }

            _context.Notes.Update(noteToUpdate);
            _context.SaveChanges();
        }
    }
}
