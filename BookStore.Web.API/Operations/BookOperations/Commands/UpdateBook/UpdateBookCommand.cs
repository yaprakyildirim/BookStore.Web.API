﻿using AutoMapper;
using BookStore.Web.API.Context;

namespace BookStore.Web.API.Operations.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public UpdateBookViewModel Model;
        public UpdateBookCommand(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(book => book.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Book doesn't exist!");

            _mapper.Map(Model, book);

            _dbContext.SaveChanges();

        }
    }

    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
