using AutoMapper;
using BookStore.Web.API.Common;
using BookStore.Web.API.Context;
using Microsoft.EntityFrameworkCore;

public class CommonTestFixture
{
	public BookDbContext Context { get; set; }
	public IMapper Mapper { get; set; }
	public CommonTestFixture()
	{
		
		var options = new DbContextOptionsBuilder<BookDbContext>().UseInMemoryDatabase("BookStoreTestDB").Options;
		Context = new BookDbContext(options);
		
		Context.Database.EnsureCreated();
		
		Context.AddBooks();
		Context.AddGenres();
		Context.AddAuthors();
		Context.SaveChanges();
		
		Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
	}	
}