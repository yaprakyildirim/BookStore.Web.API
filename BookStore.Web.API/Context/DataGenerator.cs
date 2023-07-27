using BookStore.Web.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.API.Context
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Miyamoto",
                        Surname = "Musashi",
                        Birthday = new DateTime(1995, 4, 7)
                    },
                    new Author
                    {
                        Name = "Marcus",
                        Surname = "Aurelius",
                        Birthday = new DateTime(1950, 3, 8)
                    },
                    new Author
                    {
                        Name = "Frank",
                        Surname = "Herbert",
                        Birthday = new DateTime(2001, 7, 2)
                    }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth",
                    },
                    new Genre
                    {
                        Name = "Science Fiction",
                    },
                    new Genre
                    {
                        Name = "Philosophy",
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Book of 5 Rings",
                        AuthorId = 1,
                        GenreId = 3,
                        PageCount = 128,
                        PublishDate = new DateTime(2005, 8, 7),
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Meditations",
                        AuthorId = 2,
                        GenreId = 3,
                        PageCount = 112,
                        PublishDate = new DateTime(2010, 2, 3),
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        AuthorId = 3,
                        GenreId = 2,
                        PageCount = 879,
                        PublishDate = new DateTime(2020, 5, 7),
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
