using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Models
{
    public enum ClientTypeEnum
    {
        [EnumMember(Value = "Active")]
        active,

        [EnumMember(Value = "Prospect")]
        prospect
    }

    public class ClientModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("client_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ClientTypeEnum ClientType { get; set; }
        [JsonProperty("date_of_birth")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateOfBirth { get; set; }
        public List<string> Jurisdictions { get; set; }
    }

    public class CreateClientModelRequest
    {
        public string name { get; set; }
        [JsonProperty("client_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ClientTypeEnum ClientType { get; set; }
        [JsonProperty("date_of_birth")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateOfBirth { get; set; }
        public List<string> jurisdictions { get; set; } = new List<string>();
    }

}
