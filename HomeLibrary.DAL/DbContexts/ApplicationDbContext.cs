using HomeLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.DAL.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(
          entity =>
          {
              entity.Property(p => p.TableOfContents)
                  .HasColumnType("xml");

              entity.HasOne(p => p.Author)
                  .WithMany()
                  .HasForeignKey(p => p.AuthorId)
                  .OnDelete(DeleteBehavior.Restrict);
          });
    }
}
