using BookStore.Web.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.API.Context
{
    public interface IBookDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
