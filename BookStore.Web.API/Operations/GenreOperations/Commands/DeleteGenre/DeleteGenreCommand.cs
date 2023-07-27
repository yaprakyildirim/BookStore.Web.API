using BookStore.Web.API.Context;

namespace BookStore.Web.API.Operations.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookDbContext _dbContext;

        public DeleteGenreCommand(IBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(g => g.Id == GenreId);

            if (genre is null)
                throw new InvalidOperationException("ID could not find!");

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}
