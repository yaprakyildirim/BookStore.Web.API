using FluentValidation;

namespace BookStore.Web.API.Operations.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailQueryValidator()
        {
            RuleFor(a => a.AuthorId).GreaterThan(0);
        }
    }
}
