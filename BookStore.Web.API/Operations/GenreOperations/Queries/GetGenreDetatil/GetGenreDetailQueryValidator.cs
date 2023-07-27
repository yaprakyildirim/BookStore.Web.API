using FluentValidation;

namespace BookStore.Web.API.Operations.GenreOperations.Queries.GetGenreDetatil
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(g => g.GenreId).GreaterThan(0).NotEmpty();
        }
    }
}
