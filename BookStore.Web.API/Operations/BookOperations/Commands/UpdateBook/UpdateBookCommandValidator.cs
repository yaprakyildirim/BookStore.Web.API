using FluentValidation;

namespace BookStore.Web.API.Operations.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(3);
            RuleFor(command => command.Model.PublishDate.Date).LessThan(DateTime.Now.Date);
        }
    }
}