using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIGateway
{
    public class PlayerEntity
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [JsonPropertyName("NickName")]
        public string NickName { get; set; }

        [JsonPropertyName("ListeDesParties")]
        public List<GameSimplifiedEntity> ListeDesParties { get; set; }
    }
}
