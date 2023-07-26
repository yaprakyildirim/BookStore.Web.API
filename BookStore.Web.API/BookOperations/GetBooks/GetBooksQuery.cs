using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;

namespace BookStore.Web.API.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}