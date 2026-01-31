using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using TUIToDo.DTO;
using TUIToDo.Models;

namespace TUIToDo.Storage
{
    public class Storage : IDisposable
    {
        private FileStream fileStream;
        public Storage(string path, FileMode mode, FileAccess access)
        {
            fileStream = new FileStream(path, mode, access);
        }

        public void SaveToDos(ToDoList toDoList)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() 
            { 
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            JsonSerializer.Serialize(fileStream, toDoList, options);
        }

        public ToDoList? GetToDos()
        {

            using (StreamReader sr = new StreamReader(fileStream))
            {
                string content = sr.ReadToEnd();
                var dto = JsonSerializer.Deserialize<ToDoListDTO>(content);
                ToDoList list = new ToDoList(dto.Title);

                foreach (ToDoItemDTO itemDto in dto.ToDoItems)
                {
                    var item = new ToDoItem(itemDto.Text)
                    {
                        Id = itemDto.Id,
                        DateStart = itemDto.DateStart,
                        DateEnd = itemDto.DateEnd
                    };
                    list.Add(item);
                }

                return list;
            }
        }

        public void Dispose()
        {
            Console.WriteLine("");
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                fileStream?.Dispose();
            }
        }

        ~Storage()
        {
            Dispose(true);
        }
    }
}
