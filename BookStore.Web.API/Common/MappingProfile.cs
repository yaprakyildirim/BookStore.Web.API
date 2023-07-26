using AutoMapper;
using BookStore.Web.API.BookOperations.GetBookDetail;
using BookStore.Web.API.BookOperations.GetBooks;
using BookStore.Web.API.Entities;
using static BookStore.Web.API.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.Web.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}