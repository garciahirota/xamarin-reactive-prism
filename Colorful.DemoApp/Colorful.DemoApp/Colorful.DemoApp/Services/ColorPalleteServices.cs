using Colorful.DemoApp.Contracts;
using Colorful.DemoApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Colorful.DemoApp.Services
{
    public class ColorPalleteServices : IColorPalleteServices
    {
        const string HostName = "https://www.colourlovers.com/api";
        async public Task<List<PalleteItem>> GetPalletsAsync(string search = null)
        {
            string url = string.IsNullOrEmpty(search) ? "/palettes?format=json" : "/palettes?format=json&keywords=" + search;

            using (WebClient client = new WebClient())
            {
                var json = await client.DownloadStringTaskAsync(HostName + url);
                return JsonConvert.DeserializeObject<List<PalleteItem>>(json);
            }
        }
    }
}
