using HomeLibrary.DAL.DbContexts;
using HomeLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Web.SeedData;

public static class SeedData
{
    public static async Task RunSeed(
        ApplicationDbContext dbContext, 
        int authorsCount,
        int booksCount,
        ILogger logger, 
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.Authors.AnyAsync(cancellationToken);
        if(exists )
        {
            logger.LogInformation("Seeding was skipped.");
            return;
        }

        await SeedAuthors(dbContext, authorsCount, logger, cancellationToken);
        await SeedBooks(dbContext, booksCount, logger, cancellationToken);
    }

    static async Task SeedAuthors(
        ApplicationDbContext dbContext, int count, ILogger logger, CancellationToken cancellationToken)
    {
        for (var i = 0; i < count; i++)
        {
            dbContext.Authors.Add(
                new Author()
                {
                    Name = $"author-{1 + i}",
                });
        }
        var totalCount = await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeding authors done. Records: {Count}", totalCount);
    }

    static async Task SeedBooks(
        ApplicationDbContext dbContext, int count, ILogger logger, CancellationToken cancellationToken)
    {
        var authors = await dbContext.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var no = 0;
        foreach (var author in authors) 
        {
            for (var i = 0; i < count; i++, no++)
            {
                dbContext.Books.Add(
                    new Book()
                    {
                        Title = $"book-{1 + no}",
                        AuthorId = author.Id,
                        PublishYear = 2000 + no,
                        TableOfContents = "<root/>",
                    });
            }
        }
        var totalCount = await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeding books done. Records: {Count}", totalCount);
    }
}
