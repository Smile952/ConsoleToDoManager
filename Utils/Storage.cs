using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using TUIToDo.Models;

namespace TUIToDo.Utils
{
    public class Storage
    {

        private readonly string _path;
        public Storage(string path)
        {
            _path = path;
        }

        public void SaveToDos(ToDoList toDoList)
        {
            string pathToFile = $"{_path}\\{toDoList.Title}.json";
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                JsonSerializerOptions options = new JsonSerializerOptions() 
                { 
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
                };
                JsonSerializer.Serialize(fs, toDoList, options);
            }
        }

        public ToDoList? GetToDosByTitle(string title)
        {
            string pathToFile = $"{_path}\\{title}.json";
           
            string content = File.ReadAllText(pathToFile);
            var dto = JsonSerializer.Deserialize<ToDoListDTO>(content);
            ToDoList list = new ToDoList(dto.Title);
            
            foreach (ToDoItem item in dto.ToDoItems)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
