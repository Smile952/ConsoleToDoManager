namespace TUIToDo.Models
{
    public class ToDoList
    {

        private readonly List<ToDoItem> _items = new();

        public string Title { 
            get; 
            private set {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Title cannot be empty or whitespace");
                field = value;
            } 
        }

        public IReadOnlyList<ToDoItem> Items => _items;

        public ToDoList (string Title)
        {
            this.Title = Title;
        }

        public void Add(ToDoItem item)
        {
            _items.Add(item);
        }

        public void Remove(int id)
        {
            ToDoItem? item = _items.Where(i => i.Id == id).FirstOrDefault();
            if (item == null)
                return;
            _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void MarkDoneItem(int id)
        {
            ToDoItem? item = _items.Where(i => i.Id == id).FirstOrDefault();
            if (item == null)
                return;
            item.MarkDone();
        }

        public void EditToDoText (int id, string newText)
        {
            ToDoItem? item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null) return;
            item.UpdateText(newText);
        }
    }
}
