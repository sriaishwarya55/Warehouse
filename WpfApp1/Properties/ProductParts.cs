using System.Text.Json;
using Newtonsoft.Json;


namespace WpfApp1.Properties
{
    public class ProductParts
    {
        [JsonProperty(PropertyName = "art_id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "stock")]
        public int stock { get; set; }
    }
}
