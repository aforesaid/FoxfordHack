using System.Text.Json.Serialization;
namespace FoxfordHack.Models.ModelParsingToJson.Course
{
    class Course
    {
        [JsonPropertyName("resource_id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("cart_item_type")]
        public string CartItemType { get; set; }
    }
}
