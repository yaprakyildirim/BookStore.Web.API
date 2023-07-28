using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.BookOperations.Commands.CreateBook;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace WebApi.UnitTests.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateBookCommandTests
    {
        private readonly Mock<IBookDbContext> _dbContextMock = new Mock<IBookDbContext>();
        private readonly Mock<DbSet<Book>> _dbSetMock = new Mock<DbSet<Book>>();
        private readonly IMapper _mapper;

        public CreateBookCommandTests()
        {
            // AutoMapper configuration
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateBookCommand.CreateBookViewModel, Book>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void WhenBookWithTitleAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var existingBooks = new[]
            {
                new Book { Title = "Existing Title" }
            }.AsQueryable();

            _dbSetMock.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(existingBooks.Provider);
            _dbSetMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(existingBooks.Expression);
            _dbSetMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(existingBooks.ElementType);
            _dbSetMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(existingBooks.GetEnumerator());

            _dbContextMock.Setup(db => db.Books).Returns(_dbSetMock.Object);

            var command = new CreateBookCommand(_dbContextMock.Object, _mapper)
            {
                Model = new CreateBookCommand.CreateBookViewModel
                {
                    Title = "Existing Title",
                    AuthorId = 1,
                    PublishDate = DateTime.Now,
                    GenreId = 1,
                    PageCount = 100
                }
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => command.Handle());
        }
    }
}
