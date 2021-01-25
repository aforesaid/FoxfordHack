using FoxfordHack.Models.ModelParsingToJson.Answers;
using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson.Question
{
    class BaseQuestion
    {

        [JsonPropertyName("Header")]
        public string Header { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("answers")]
        public object ObjectAnswers { get; set; }
        [JsonPropertyName("correct_answers")]
        public object ObjectAnswersByText { get; set; }
        [JsonPropertyName("given_answers")]
        public object ObjectAnswersByTextSelection { get; set; }
        [JsonPropertyName("groups")]
        public object ObjectAnswersByMatchGroup { get; set; }
    }
}
