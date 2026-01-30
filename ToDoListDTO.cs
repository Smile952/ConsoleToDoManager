using System.Text.Json.Serialization;
using TUIToDo.Models;

namespace TUIToDo
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("Items")]
        public List<ToDoItem> ToDoItems { get; set; }
    }
}
