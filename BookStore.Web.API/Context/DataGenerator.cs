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

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,//personal growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                     new Book
                     {
                         Title = "Herland",
                         GenreId = 2, //Science
                         PageCount = 250,
                         PublishDate = new DateTime(2005, 07, 01)
                     },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 3, //Science Fiction
                        PageCount = 300,
                        PublishDate = new DateTime(2008, 07, 01)
                    });

                context.SaveChanges();
            }
        }
    }
}
