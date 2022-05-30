using Microsoft.EntityFrameworkCore;
using Notebook.Areas.Identity.Data;
using Notebook.Models;
using Notebook.Pages;
using Notebook.Repositories;
using Notebook.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("NotebookContextConnection") ?? throw new InvalidOperationException("Connection string 'NotebookContextConnection' not found.");

builder.Services.AddDbContext<NotebookContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<NotebookContext>();
builder.Services.AddTransient<LabelsRepository>();
builder.Services.AddTransient<NotesRepository>();
builder.Services.AddTransient<UserService>();
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
