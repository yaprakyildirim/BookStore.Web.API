using BookStore.Web.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.API.Context
{
    public class BookDbContext : DbContext, IBookDbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
