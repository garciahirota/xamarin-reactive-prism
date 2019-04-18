using System.Threading.Tasks;

namespace Colorful.DemoApp.Contracts
{
    public interface ILoginServices
    {
        Task<bool> LoginAsync(string username, string password);
    }
}
