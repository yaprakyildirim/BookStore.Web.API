using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStore.Web.API.Services
{
    //ILoggerService arayüzü, uygulama içinde farklı loglama hizmetleri için kullanılır
    // Bu arayüzü uygulayan sınıflar, loglama işlevselliğini sağlamak için Write() yöntemini uygulamak zorundadır.
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
