using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.AuthorOperations.Commands.DeleteAuthor;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace WebApi.UnitTests.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests
    {
        private readonly Mock<IBookDbContext> _dbContextMock = new Mock<IBookDbContext>();

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> elements) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elements.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elements.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elements.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elements.GetEnumerator());

            return dbSetMock;
        }

        [Fact]
        public void WhenAuthorIdIsNotFound_ThrowsInvalidOperationException()
        {
            var authorsDbSetMock = CreateDbSetMock(new Author[] { }.AsQueryable());
            _dbContextMock.Setup(db => db.Authors).Returns(authorsDbSetMock.Object);

            var command = new DeleteAuthorCommand(_dbContextMock.Object)
            {
                AuthorId = 999  // This ID doesn't exist.
            };

            Assert.Throws<InvalidOperationException>(() => command.Handle());
        }

        [Fact]
        public void WhenAuthorHasPublishedBook_ThrowsInvalidOperationException()
        {
            var authors = new[]
            {
                new Author { Id = 1, Name = "John", Surname = "Doe" }
            }.AsQueryable();

            var books = new[]
            {
                new Book { AuthorId = 1, Title = "Sample Book" }
            }.AsQueryable();

            var authorsDbSetMock = CreateDbSetMock(authors);
            var booksDbSetMock = CreateDbSetMock(books);

            _dbContextMock.Setup(db => db.Authors).Returns(authorsDbSetMock.Object);
            _dbContextMock.Setup(db => db.Books).Returns(booksDbSetMock.Object);

            var command = new DeleteAuthorCommand(_dbContextMock.Object)
            {
                AuthorId = 1
            };

            Assert.Throws<InvalidOperationException>(() => command.Handle());
        }

        [Fact]
        public void WhenAuthorHasNoPublishedBooks_AuthorIsDeletedSuccessfully()
        {
            var authors = new[]
            {
                new Author { Id = 2, Name = "Jane", Surname = "Smith" }
            }.AsQueryable();

            var books = new Book[] { }.AsQueryable();

            var authorsDbSetMock = CreateDbSetMock(authors);
            var booksDbSetMock = CreateDbSetMock(books);

            _dbContextMock.Setup(db => db.Authors).Returns(authorsDbSetMock.Object);
            _dbContextMock.Setup(db => db.Books).Returns(booksDbSetMock.Object);

            var command = new DeleteAuthorCommand(_dbContextMock.Object)
            {
                AuthorId = 2
            };

            command.Handle();

            authorsDbSetMock.Verify(set => set.Remove(It.IsAny<Author>()), Times.Once);
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }
    }
}