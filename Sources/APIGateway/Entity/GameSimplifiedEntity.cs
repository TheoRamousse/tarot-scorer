using System.Text.Json.Serialization;

namespace APIGateway
{
    public class GameSimplifiedEntity
    {
        [JsonPropertyName("GameId")]
        public long Id { get; set; }
    }
}