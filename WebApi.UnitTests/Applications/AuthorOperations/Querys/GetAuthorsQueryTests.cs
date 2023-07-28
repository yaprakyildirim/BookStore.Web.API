using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.UnitTests.Applications.AuthorOperations.Querys
{
    public class GetAuthorsQueryTests
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
        public void WhenAuthorsExist_ReturnsListOfAuthorsViewModel()
        {
            var authors = new[]
            {
                new Author { Id = 1, Name = "John", Surname = "Doe", Birthday = DateTime.Parse("1980-01-01") },
                new Author { Id = 2, Name = "Jane", Surname = "Smith", Birthday = DateTime.Parse("1985-05-05") }
            }.AsQueryable();

            var authorsDbSetMock = CreateDbSetMock(authors);
            _dbContextMock.Setup(db => db.Authors).Returns(authorsDbSetMock.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<List<GetAuthorsQuery.AuthorsViewModel>>(It.IsAny<List<Author>>()))
                      .Returns(new List<GetAuthorsQuery.AuthorsViewModel>
                      {
                          new GetAuthorsQuery.AuthorsViewModel
                          {
                              Name = "John",
                              Surname = "Doe",
                              Birthday = "1980-01-01"
                          },
                          new GetAuthorsQuery.AuthorsViewModel
                          {
                              Name = "Jane",
                              Surname = "Smith",
                              Birthday = "1985-05-05"
                          }
                      });

            var query = new GetAuthorsQuery(_dbContextMock.Object, mockMapper.Object);
            var result = query.Handle();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].Name);
            Assert.Equal("Doe", result[0].Surname);
            Assert.Equal("1980-01-01", result[0].Birthday);
            Assert.Equal("Jane", result[1].Name);
            Assert.Equal("Smith", result[1].Surname);
            Assert.Equal("1985-05-05", result[1].Birthday);
        }
    }
}
