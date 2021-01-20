using System;
using System.Collections.Generic;
using System.Text.Json.Serialization ;

namespace FoxfordHack.Models.ModelParsingToJson.Lesson
{
    class Lesson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("is_locked")]
        public bool IsLocked { get; set; }
    }
}
