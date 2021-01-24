using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoxfordHack.Models.ModelParsingToJson.Course
{
    class ViewCourses
    {
        [JsonPropertyName("bookmarks")]
        public List<Course> AciveCourses { get; set; }
    }
}
