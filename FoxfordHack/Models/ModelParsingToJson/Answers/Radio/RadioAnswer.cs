using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson.Answers.Radio
{
    class RadioAnswer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
        [JsonPropertyName("correct")]
        public bool IsCorrect { get; set; }
    }
}
