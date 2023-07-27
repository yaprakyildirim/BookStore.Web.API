using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Operations.AuthorOperations.Commands.CreateAuthor;
using BookStore.Web.API.Operations.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Web.API.Operations.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Web.API.Operations.AuthorOperations.Queries.GetAuthorDetail;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Web.API.Operations.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore.Web.API.Operations.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace BookStore.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            query.AuthorId = id;

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);

            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            command.AuthorId = id;
            command.Handle();

            return Ok();
        }
    }
}
