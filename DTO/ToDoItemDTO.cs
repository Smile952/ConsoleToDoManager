namespace TUIToDo.DTO
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsDone { get; set; }
    }
}
