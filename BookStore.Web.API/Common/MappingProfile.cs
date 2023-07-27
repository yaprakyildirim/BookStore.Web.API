using AutoMapper;
using BookStore.Web.API.Entities;
using BookStore.Web.API.Operations.BookOperations.Commands.CreateBook;
using BookStore.Web.API.Operations.BookOperations.Commands.UpdateBook;
using BookStore.Web.API.Operations.BookOperations.Queries;
using static BookStore.Web.API.Operations.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore.Web.API.Operations.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static BookStore.Web.API.Operations.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static BookStore.Web.API.Operations.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStore.Web.API.Operations.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static BookStore.Web.API.Operations.GenreOperations.Queries.GetGenreDetatil.GetGenreDetailQuery;
using static BookStore.Web.API.Operations.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static GetAuthorsQuery;

namespace BookStore.Web.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<CreateBookCommand.CreateBookViewModel, Book>();
            CreateMap<UpdateBookViewModel, Book>();

            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreViewModel, Genre>();
            CreateMap<UpdateGenreViewModel, Genre>();

            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorViewModel, Author>();
            CreateMap<UpdateAuthorViewModel, Author>();
        }
    }
}