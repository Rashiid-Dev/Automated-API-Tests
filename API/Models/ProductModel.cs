using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Jurisdictions { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        [JsonProperty("date_created")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateCreated { get; set; }
    }
}
