using System.Text.Json.Serialization;

namespace Hearthstone.Models
{
    public class Set
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        [JsonPropertyName("collectibleCount")]
        public int CardCount { get; set; }
    }
}
