using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;

public static class Genres
{
	public static void AddGenres(this BookDbContext context)
	{
		context.Genres.AddRange(
			new Genre{ Name = "Personal Growth", },
			new Genre{ Name = "Science Fiction", },
			new Genre{ Name = "Philosophy", }
		);
	}
}