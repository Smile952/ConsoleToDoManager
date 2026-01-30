namespace TUIToDo.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Text { 
            get; 
            private set { 
                if(String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("ToDo text cannot be empty or whitespace");
                field = value;
            } 
        }

        

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsDone { get; private set; }

        public ToDoItem()
        {

        }

        public ToDoItem(string text)
        {
            Text = text;
        }

        public ToDoItem(string text, DateTime DateStart, DateTime DateEnd)
        {
            Text = text;
            this.DateStart = DateStart;
            this.DateEnd = DateEnd;
        }


        public void UpdateText(string text)
        {
            Text = text;
        }

        public void MarkDone()
        {
            IsDone = true;
        }
    }
}
