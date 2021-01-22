using System;
using System.Collections.Generic;
using System.Text.Json.Serialization ;

namespace FoxfordHack.Models.ModelParsingToJson.Answers.Text
{
    class TextAnswer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("correct_answers")]
        public List<string> CorrectAnswers { get; set; }
    }
}
