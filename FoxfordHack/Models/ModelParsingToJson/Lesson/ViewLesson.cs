using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Lesson
{
    class ViewLesson
    {
        [JsonPropertyName("lessons")]
        public List<Lesson> Lessons { get; set; }
        [JsonPropertyName("cursors")]
        public Cursor Cursor { get; set; }
    }
}
