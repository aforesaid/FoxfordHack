using System;
using System.Collections.Generic;
using System.Text.Json.Serialization ;

namespace FoxfordHack.Models.ModelParsingToJson.Answers.TextGap
{
    class TextGapAnswer
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        [JsonPropertyName("correct")]
        public bool IsCorrect { get; set; }
    }
}
