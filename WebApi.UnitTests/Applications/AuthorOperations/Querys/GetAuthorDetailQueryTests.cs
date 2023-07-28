using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.AuthorOperations.Queries.GetAuthorDetail;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace WebApi.UnitTests.Applications.AuthorOperations.Querys
{
    public class GetAuthorDetailQueryTests
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

            var query = new GetAuthorDetailQuery(_dbContextMock.Object, null) // Here, I'm passing null for IMapper since we are not testing the mapping functionality
            {
                AuthorId = 999  // This ID doesn't exist.
            };

            Assert.Throws<InvalidOperationException>(() => query.Handle());
        }

        [Fact]
        public void WhenAuthorIdIsFound_ReturnsAuthorDetailViewModel()
        {
            var authors = new[]
            {
                new Author { Id = 1, Name = "John", Surname = "Doe", Birthday = DateTime.Parse("1980-01-01") }
            }.AsQueryable();

            var authorsDbSetMock = CreateDbSetMock(authors);
            _dbContextMock.Setup(db => db.Authors).Returns(authorsDbSetMock.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<GetAuthorDetailQuery.AuthorDetailViewModel>(It.IsAny<Author>()))
                      .Returns(new GetAuthorDetailQuery.AuthorDetailViewModel
                      {
                          Name = "John",
                          Surname = "Doe",
                          Birthday = "1980-01-01"
                      });

            var query = new GetAuthorDetailQuery(_dbContextMock.Object, mockMapper.Object)
            {
                AuthorId = 1
            };

            var result = query.Handle();
            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
            Assert.Equal("Doe", result.Surname);
            Assert.Equal("1980-01-01", result.Birthday);
        }
    }
}
