using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Answers.TextCompose
{
    class TextComposeAnswer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("correct_answers")]
        public List<string> Answers { get; set; } = new List<string>();
    }
}
