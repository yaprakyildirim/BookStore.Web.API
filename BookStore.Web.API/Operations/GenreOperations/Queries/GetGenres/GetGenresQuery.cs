using AutoMapper;
using BookStore.Web.API.Context;

namespace BookStore.Web.API.Operations.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var genreList = _dbContext.Genres.Where(g => g.IsActive == true).OrderBy(g => g.Id).ToList();
            List<GenreViewModel> viewModel = _mapper.Map<List<GenreViewModel>>(genreList);

            return viewModel;
        }

        public class GenreViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
