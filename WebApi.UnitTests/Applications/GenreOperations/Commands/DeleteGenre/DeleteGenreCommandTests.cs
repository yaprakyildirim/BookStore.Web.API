using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.GenreOperations.Commands.DeleteGenre;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.UnitTests.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests
    {
        private readonly Mock<IBookDbContext> _dbContextMock = new Mock<IBookDbContext>();
        private readonly Mock<DbSet<Genre>> _genreDbSetMock = new Mock<DbSet<Genre>>();

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            // Arrange
            var genreId = 1;
            var genre = new Genre { Id = genreId, Name = "Fiction" };

            var genreList = new List<Genre> { genre }.AsQueryable();

            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.Provider).Returns(genreList.Provider);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.Expression).Returns(genreList.Expression);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.ElementType).Returns(genreList.ElementType);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.GetEnumerator()).Returns(genreList.GetEnumerator());

            _dbContextMock.Setup(db => db.Genres).Returns(_genreDbSetMock.Object);

            var deleteGenreCommand = new DeleteGenreCommand(_dbContextMock.Object)
            {
                GenreId = genreId
            };

            // Act
            deleteGenreCommand.Handle();

            // Assert
            _genreDbSetMock.Verify(db => db.Remove(It.IsAny<Genre>()), Times.Once());
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once());
        }
    }
}
