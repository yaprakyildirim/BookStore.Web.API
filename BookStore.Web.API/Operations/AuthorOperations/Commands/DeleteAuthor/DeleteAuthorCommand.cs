﻿using BookStore.Web.API.Context;

namespace BookStore.Web.API.Operations.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookDbContext _dbContext;

        public DeleteAuthorCommand(IBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);
            var authorBooks = _dbContext.Books.SingleOrDefault(a => a.AuthorId == AuthorId);

            if (author is null)
                throw new InvalidOperationException("ID isn't found.");

            if (authorBooks is not null)
                throw new InvalidOperationException(author.Name + " " + author.Surname + " has a published book. Please delete book first.");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
