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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
