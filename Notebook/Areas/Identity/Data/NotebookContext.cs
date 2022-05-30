using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notebook.Areas.Identity.Data;

namespace Notebook.Models;

public class NotebookContext : IdentityDbContext<User>
{
    public DbSet<Label> Labels { get; set; }
    public DbSet<Note> Notes { get; set; }

    public NotebookContext(DbContextOptions<NotebookContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
