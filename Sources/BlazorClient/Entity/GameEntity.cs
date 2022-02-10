using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorClient.Entity
{
    public class GameEntity
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }

        [JsonPropertyName("TakerPoints")]
        public long TakerPoints { get; set; }

        [JsonPropertyName("Excuse")]
        public bool Excuse { get; set; }

        [JsonPropertyName("TwentyOne")]
        public bool TwentyOne { get; set; }

        [JsonPropertyName("PetitResult")]
        public string PetitResult { get; set; }

        [JsonPropertyName("Poignée")]
        public string Poignée { get; set; }

        [JsonPropertyName("Chelem")]
        public string Chelem { get; set; }

        [JsonPropertyName("NbPlayers")]
        public string NbPlayers { get; set; }

        [JsonPropertyName("Rules")]
        public string Rules { get; set; }
    }
}
