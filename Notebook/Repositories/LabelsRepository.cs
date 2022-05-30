using Microsoft.EntityFrameworkCore;
using Notebook.Models;

namespace Notebook.Repositories
{
    public class LabelsRepository
    {
        private readonly NotebookContext _context;

        public LabelsRepository(NotebookContext context)
        {
            _context = context;
        }

        public List<Label> GetLabels(string UserId, string labelTitle = null)
        {
           var labels = _context.Labels.Include(l => l.Notes).Where(l => l.UserId == UserId).ToList();

            if (!string.IsNullOrEmpty(labelTitle))
            {
                labels = labels.Where(l => l.Title.Contains(labelTitle, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            return labels.OrderBy(l => l.Id).ToList();
        }

        public void CreateLabel(Label label)
        {
            if (label == null)
            {
                throw new ArgumentNullException();
            }
            _context.Labels.Add(label);
            _context.SaveChanges();
        }

        public Label GetLabel(int id)
        {
            return _context.Labels.Include(l => l.Notes).FirstOrDefault(l => l.Id == id);
        }

        public void DeleteLabel(int id)
        {
            var label = GetLabel(id);
            if (label == null)
            {
                throw new ArgumentNullException($"Label with id {id} was not found!");
            }

            var labelNotes = _context.Notes.Where(n => n.LabelId == label.Id);
            if (labelNotes.Count() > 0) _context.Notes.RemoveRange(labelNotes);
            _context.Labels.Remove(label);
            _context.SaveChanges();
        }

        public void UpdateLabel(Label label)
        {
            if (label == null)
            {
                throw new ArgumentNullException();
            }

            var labelToUpdate = GetLabel(label.Id);
            if (labelToUpdate == null)
            {
                throw new ArgumentNullException();
            }

            _context.Labels.Update(labelToUpdate);
            _context.SaveChanges();
        }
    }
}
