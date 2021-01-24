using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Answers.Links
{
    class LinksAnswer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("correct_answer_ids")]
        public List<int> CorrectAnswersId { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
