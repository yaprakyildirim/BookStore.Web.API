using BookStore.Web.API.Context;

namespace BookStore.Web.API.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı!");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}
