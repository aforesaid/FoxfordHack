using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson.Answers
{
    public enum ANSWER_TYPES
    {
        [JsonPropertyName("text_gap")]
        TEXT_GAP = 0,
        [JsonPropertyName("checkbox")]
        CHECKBOX = 1,
        [JsonPropertyName("radio")]
        RADIO = 2,
        [JsonPropertyName("links")]
        LINKS = 3,
        [JsonPropertyName("text")]
        TEXT = 4
    }
}
