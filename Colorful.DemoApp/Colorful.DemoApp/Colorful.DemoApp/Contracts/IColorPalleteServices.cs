using Colorful.DemoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colorful.DemoApp.Contracts
{
    public interface IColorPalleteServices
    {
        Task<List<PalleteItem>> GetPalletsAsync(string search = null);
    }
}
