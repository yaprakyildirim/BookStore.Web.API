using BookStore.Web.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStore.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,//personal growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12)
            },
             new Book{
                Id = 2,
                Title = "Herland",
                GenreId = 2, //Science
                PageCount = 250,
               PublishDate = new DateTime(2005,07,01)
            },
            new Book{
                Id = 3,
                Title = "Dune",
                GenreId = 3, //Science Fiction
                PageCount = 300,
               PublishDate = new DateTime(2008, 07, 01)
            }

        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }
    }
}
