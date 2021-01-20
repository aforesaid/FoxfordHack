using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Answers.CheckboxType
{
    class CheckboxAnswer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("correct")]
        public bool IsCorrect { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
    }
}
