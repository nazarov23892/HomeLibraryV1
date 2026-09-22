using HomeLibrary.AL.Repositories;
using HomeLibrary.AL.Services;
using HomeLibrary.AL.Services.Concrete;
using HomeLibrary.DAL.Concrete;
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

// Services;
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();
