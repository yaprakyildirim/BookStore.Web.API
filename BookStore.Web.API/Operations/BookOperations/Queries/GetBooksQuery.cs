using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Web.API.Operations.BookOperations.Queries
{
    public class GetBooksQuery
    {
        private readonly IBookDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(g => g.Genre).Include(a => a.Author).OrderBy(x => x.Id).ToList();
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
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }
}