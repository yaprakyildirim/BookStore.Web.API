namespace BookStore.Web.API.Services
{
    public class DBLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DBLogger] - " + message);
        }
    }
}
