using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.GenreOperations.Commands.CreateGenre;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace WebApi.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests
    {
        private readonly Mock<IBookDbContext> _dbContextMock = new Mock<IBookDbContext>();
        private readonly Mock<DbSet<Genre>> _genreDbSetMock = new Mock<DbSet<Genre>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        [Fact]
        public void WhenValidGenreIsGiven_Genre_ShouldBeAdded()
        {
            // Arrange
            var genreName = "Fiction";

            var genreList = new List<Genre>().AsQueryable();

            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.Provider).Returns(genreList.Provider);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.Expression).Returns(genreList.Expression);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.ElementType).Returns(genreList.ElementType);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.GetEnumerator()).Returns(genreList.GetEnumerator());

            _dbContextMock.Setup(db => db.Genres).Returns(_genreDbSetMock.Object);

            var newGenreViewModel = new CreateGenreCommand.CreateGenreViewModel { Name = genreName, IsActive = true };
            var newGenre = new Genre { Name = genreName, IsActive = true };

            _mapperMock.Setup(m => m.Map<Genre>(newGenreViewModel)).Returns(newGenre);

            var createGenreCommand = new CreateGenreCommand(_dbContextMock.Object, _mapperMock.Object)
            {
                Model = newGenreViewModel
            };

            // Act
            createGenreCommand.Handle();

            // Assert
            _genreDbSetMock.Verify(db => db.Add(It.IsAny<Genre>()), Times.Once());
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once());
        }
    }
}
