using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
    public class SalesModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string ClientId { get; set; }
        public string ProductId { get; set; }
        [JsonProperty("date_created")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateCreated { get; set; }
    }
}
