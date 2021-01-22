using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson
{
    class TaskFoxford
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
