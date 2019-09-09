using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;

namespace BagelBoss.API.API.Entities
{
    public class Bagel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public String Store { get; set; }

        public String Schmear { get; set; }

        [JsonIgnore]
        public PartitionKey PartitionKey
        {
            get
            {
                return new PartitionKey(Store);
            }
        }
    }
}