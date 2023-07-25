using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.API.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }

        private readonly BookDbContext _dbContext;
        public CreateBookCommand(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
                throw new InvalidOperationException("Bu kitap zaten mevcut!");

            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;


            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
