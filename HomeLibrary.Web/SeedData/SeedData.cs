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

        var bookNo = 0;
        foreach (var author in authors) 
        {
            for (var i = 0; i < count; i++, bookNo++)
            {
                var bookTitle = $"book-{1 + bookNo}";
                var tableOfContent =
$"""
<content>
<h5>Part I. The Beginning{bookNo}</h5>
<ul>
    <li>Chapter{bookNo}1. — {bookNo + 7}</li>
    <li>Chapter{bookNo}2. — {bookNo + 23}</li>
    <li>Chapter{bookNo}3. — {bookNo + 41}</li>
</ul>

<h5>Part II. The Journey{bookNo}</h5>
<ul>
    <li>Chapter{bookNo}4. TheNorthWind{bookNo} — {bookNo + 59}</li>
    <li>
        Chapter{bookNo}5. TheMapThatExist{bookNo} — {bookNo + 78}
        <ul>
            <li>5.1. TheOldCaretaker{bookNo} — {bookNo + 85}</li>
            <li>5.2. TheUndergroundPassage{bookNo} — {bookNo + 94}</li>
        </ul>
    </li>
    <li>Chapter{bookNo}6. ShadowsofDepartedCities{bookNo} — {bookNo + 112}</li>
</ul>

<h5>Part III. The Return{bookNo}</h5>
<ul>
    <li>Chapter{bookNo}7. TheLongRoadHome{bookNo} — {bookNo + 143}</li>
    <li>Chapter{bookNo}8. Epilogue{bookNo} — {bookNo + 167}</li>
</ul>
<ul>
    <li>AboutTheAuthor{author.Id} {author.Name} — {bookNo + 189}</li>
</ul>
</content>
""";


                dbContext.Books.Add(
                    new Book()
                    {
                        Title = bookTitle,
                        AuthorId = author.Id,
                        PublishYear = 2000 + bookNo,
                        TableOfContents = tableOfContent,
                    });
            }
        }
        var totalCount = await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeding books done. Records: {Count}", totalCount);
    }
}
