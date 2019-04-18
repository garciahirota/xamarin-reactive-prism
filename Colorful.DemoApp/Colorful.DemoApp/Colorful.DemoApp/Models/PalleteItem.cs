using System.Collections.Generic;

namespace Colorful.DemoApp.Models
{
    public class PalleteItem
    {
        public string id { get; set; }
        public string title { get; set; }
        public string userName { get; set; }
        public string numViews { get; set; }
        public string numVotes { get; set; }
        public string numComments { get; set; }
        public string numHearts { get; set; }
        public string rank { get; set; }
        public string dateCreated { get; set; }
        public List<string> colors { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public string badgeUrl { get; set; }
        public string apiUrl { get; set; }
    }
}
