using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.GenreOperations.Commands.UpdateGenre;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests
    {
        private readonly Mock<IBookDbContext> _dbContextMock = new Mock<IBookDbContext>();
        private readonly Mock<DbSet<Genre>> _genreDbSetMock = new Mock<DbSet<Genre>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        [Fact]
        public void WhenValidGenreIsGiven_Genre_ShouldBeUpdated()
        {
            // Arrange
            var genreId = 1;
            var existingGenre = new Genre { Id = genreId, Name = "Fiction", IsActive = true };
            var genreList = new List<Genre> { existingGenre }.AsQueryable();

            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.Provider).Returns(genreList.Provider);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.Expression).Returns(genreList.Expression);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.ElementType).Returns(genreList.ElementType);
            _genreDbSetMock.As<IQueryable<Genre>>().Setup(m => m.GetEnumerator()).Returns(genreList.GetEnumerator());

            _dbContextMock.Setup(db => db.Genres).Returns(_genreDbSetMock.Object);

            var updatedGenreViewModel = new UpdateGenreCommand.UpdateGenreViewModel { Name = "Updated Fiction", IsActive = true };

            _mapperMock.Setup(m => m.Map(updatedGenreViewModel, existingGenre)).Returns(existingGenre);

            var updateGenreCommand = new UpdateGenreCommand(_dbContextMock.Object, _mapperMock.Object)
            {
                GenreId = genreId,
                Model = updatedGenreViewModel
            };

            // Act
            updateGenreCommand.Handle();

            // Assert
            Assert.Equal(updatedGenreViewModel.Name, existingGenre.Name);
            _dbContextMock.Verify(db => db.SaveChanges(), Times.Once());
        }
    }
}
