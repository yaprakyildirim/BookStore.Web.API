﻿using FluentValidation;

namespace BookStore.Web.API.Operations.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(a => a.Model.Name).MinimumLength(2).NotEmpty();
            RuleFor(a => a.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(a => a.Model.Birthday.Date).LessThan(DateTime.Now.Date);
        }
    }
}
