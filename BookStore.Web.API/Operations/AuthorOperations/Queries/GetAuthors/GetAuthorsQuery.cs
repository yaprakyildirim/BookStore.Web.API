using AutoMapper;
using BookStore.Web.API.Context;

public class GetAuthorsQuery
{
    private readonly IBookDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IBookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authorList = _dbContext.Authors.OrderBy(a => a.Id).ToList();

        List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(authorList);

        return viewModel;
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
    }
}