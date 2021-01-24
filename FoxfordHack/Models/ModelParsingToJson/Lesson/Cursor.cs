using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Lesson
{
    class Cursor
    {
        [JsonPropertyName("before")]
        public int? Before { get; set; }
        [JsonPropertyName("after")]
        public int? After { get; set; }

    }
}
