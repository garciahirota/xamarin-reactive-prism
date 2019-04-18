using Colorful.DemoApp.Contracts;
using System.Threading.Tasks;

namespace Colorful.DemoApp.Services
{
    public class LoginServices : ILoginServices
    {
        async public Task<bool> LoginAsync(string username, string password)
        {
            await Task.Delay(2000);
            return username == "user@domain.com" && password == "2bad4you";
        }
    }
}
