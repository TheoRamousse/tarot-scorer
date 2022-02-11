using System.Text.Json.Serialization;

namespace BlazorClient
{
    public class GameSimplifiedEntity
    {
        [JsonPropertyName("GameId")]
        long Id { get; set; }
    }
}