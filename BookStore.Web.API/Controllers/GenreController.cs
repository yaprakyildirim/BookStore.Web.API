using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Operations.GenreOperations.Commands.CreateGenre;
using BookStore.Web.API.Operations.GenreOperations.Commands.DeleteGenre;
using BookStore.Web.API.Operations.GenreOperations.Commands.UpdateGenre;
using BookStore.Web.API.Operations.GenreOperations.Queries.GetGenreDetatil;
using BookStore.Web.API.Operations.GenreOperations.Queries.GetGenres;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Web.API.Operations.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace BookStore.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreController(IBookDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbContext, _mapper);
            return Ok(query.Handle());
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
            query.GenreId = id;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreViewModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreCommand.UpdateGenreViewModel updatedGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext, _mapper);

            command.GenreId = id;
            command.Model = updatedGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);

            command.GenreId = id;
            command.Handle();

            return Ok();
        }
    }
}