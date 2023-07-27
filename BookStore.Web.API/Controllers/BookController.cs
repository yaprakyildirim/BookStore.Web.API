using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Operations.BookOperations.Commands.CreateBook;
using BookStore.Web.API.Operations.BookOperations.Commands.DeleteBook;
using BookStore.Web.API.Operations.BookOperations.Commands.UpdateBook;
using BookStore.Web.API.Operations.BookOperations.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Web.API.Operations.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);

            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.Model = updatedBook;
            command.BookId = id;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}