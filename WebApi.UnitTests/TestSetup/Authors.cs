using BookStore.Web.API.Context;
using BookStore.Web.API.Entities;

public static class Authors 
{
	public static void AddAuthors(this BookDbContext context)
    {
    	context.Authors.AddRange(
			new Author{Name = "Miyamoto",Surname = "Musashi",Birthday = new DateTime(1600, 1, 1)},
			new Author{Name = "Marcus",Surname = "Aurelius",Birthday = new DateTime(45, 1, 1)},
			new Author{Name = "Frank",Surname = "Herbert",Birthday = new DateTime(1939, 1, 1)}
		);
    }
}