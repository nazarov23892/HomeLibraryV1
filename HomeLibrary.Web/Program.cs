using HomeLibrary.AL.Repositories;
using HomeLibrary.AL.Services;
using HomeLibrary.AL.Services.Concrete;
using HomeLibrary.DAL.Concrete;
using HomeLibrary.DAL.DbContexts;
using HomeLibrary.DAL.Models;
using HomeLibrary.Web.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF context.
var connectionString = builder.Configuration.GetConnectionString("HomeLibrary");
builder.Services.AddDbContext<ApplicationDbContext>(
    opts =>
    {
        opts.UseSqlServer(connectionString);
    });

builder.Services.Configure<DatabaseOptions>(x => x.ConnectionString = connectionString!);

// Services;
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IXmlValidator, XmlValidator>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedData.RunSeed(dbContext, authorsCount: 5, booksCount: 10, logger);
}

app.Run();
