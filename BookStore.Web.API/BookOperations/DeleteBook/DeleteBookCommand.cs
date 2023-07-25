using BookStore.Web.API.Context;

namespace BookStore.Web.API.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
