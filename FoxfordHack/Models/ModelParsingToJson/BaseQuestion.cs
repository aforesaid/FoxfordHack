using FoxfordHack.Models.ModelParsingToJson.Answers;
using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson.Questions
{
    class BaseQuestion
    {
        [JsonPropertyName("answers")]
        public object ObjectAnswers { get; set; }
        [JsonPropertyName("critical_errors_count")]
        public int MaxErrorCount { get; set; }
        [JsonPropertyName("Header")]
        public string Header { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public ANSWER_TYPES Type { get; set; }
    }
}
