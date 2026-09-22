using HomeLibrary.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF context.
var connectionString = builder.Configuration.GetConnectionString("HomeLibrary");
builder.Services.AddDbContext<ApplicationDbContext>(
    opts =>
    {
        opts.UseSqlServer(connectionString);
    });

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
