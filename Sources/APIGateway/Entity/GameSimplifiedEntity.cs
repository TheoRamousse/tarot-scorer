using System.Text.Json.Serialization;

namespace APIGateway
{
    public class GameSimplifiedEntity
    {
        [JsonPropertyName("GameId")]
        long Id { get; set; }
    }
}