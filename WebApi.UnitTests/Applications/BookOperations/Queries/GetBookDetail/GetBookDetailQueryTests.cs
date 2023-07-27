using AutoMapper;
using BookStore.Web.API.Context;
using BookStore.Web.API.Operations.BookOperations.Queries;
using FluentAssertions;
using Xunit;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture> 
{
	private readonly IBookDbContext _dbContext;
	private readonly IMapper _mapper;
	public GetBookDetailQueryTests(CommonTestFixture testFixture)
	{
		_dbContext = testFixture.Context;
		_mapper = testFixture.Mapper;
	}
	
	[Fact]
	public void WhenNonValidIdIsGiven_Book_ShoulBeReturn()
	{
		GetBookDetailQuery query = new GetBookDetailQuery(_dbContext, _mapper);
		
		
		FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("ID's not correct!");
	}
}