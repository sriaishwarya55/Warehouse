using System.Text.Json;
using Newtonsoft.Json;

namespace WpfApp1.Properties
{
    public class ArticleSet
    {
        [JsonProperty(PropertyName = "art_id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "amount_of")]
        public int amount { get; set; }

    }
}