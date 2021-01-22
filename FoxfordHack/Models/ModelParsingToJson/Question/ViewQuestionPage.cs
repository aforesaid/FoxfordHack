using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson.Question
{
    class ViewQuestionPage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("available_xp")]
        public int Xp { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("questions")]
        public List<BaseQuestion> Question { get; set; }
    }
}
