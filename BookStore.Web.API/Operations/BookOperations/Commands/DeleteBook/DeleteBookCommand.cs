using BookStore.Web.API.Context;

namespace BookStore.Web.API.Operations.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookDbContext _dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(IBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Book doesn't exist!");


            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
