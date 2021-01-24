using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Answers.TextSelection
{
    class TextSelectionAnswer
    {
        [JsonPropertyName("position")]
        public string Position { get; set; }
        [JsonPropertyName("selection")]
        public string Selection { get; set; }
    }
}
