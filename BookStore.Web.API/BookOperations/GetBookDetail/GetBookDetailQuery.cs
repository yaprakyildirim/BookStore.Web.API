using BookStore.Web.API.Common;
using BookStore.Web.API.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.API.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookDbContext _context;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı!");

            BookDetailViewModel vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            vm.Genre = ((GenreEnum)book.GenreId).ToString();
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
