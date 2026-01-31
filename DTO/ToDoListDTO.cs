using System.Text.Json.Serialization;

namespace TUIToDo.DTO
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("Items")]
        public List<ToDoItemDTO> ToDoItems { get; set; }
    }
}
