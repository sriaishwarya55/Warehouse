using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;

namespace WpfApp1.Properties
{
    public class Products
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "contain_articles")]
        public List<ArticleSet> articleSet { get; set; }

        public int availableStock { get; set; }
    }
}
