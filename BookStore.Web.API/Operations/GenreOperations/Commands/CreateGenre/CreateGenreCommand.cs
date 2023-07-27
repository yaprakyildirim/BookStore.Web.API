using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;

namespace BookStore.Web.API.Operations.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel Model { get; set; }
        private readonly IBookDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreCommand(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(g => g.Name == Model.Name);

            if (genre is not null)
                throw new InvalidOperationException("Genre is already added!");

            genre = _mapper.Map<Genre>(Model);

            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
        public class CreateGenreViewModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
