using FluentValidation;

namespace BookStore.Web.API.Operations.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(cmd => cmd.AuthorId).GreaterThan(0);
        }
    }
}
