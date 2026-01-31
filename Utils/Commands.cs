using TUIToDo.Models;
using TUIToDo.Storage;

namespace TUIToDo.Utils
{
    public static class Commands
    {
        public static void DisplayToDos(string taskList)
        {
            string fullPath = $"{Directory.GetCurrentDirectory()}\\{taskList}.json";

            using (var storage = new Storage.Storage(fullPath, FileMode.Open, FileAccess.Read)) 
            {
                var todos = storage.GetToDos();

                if (todos.Items.Count == 0)
                {
                    Console.WriteLine($"Список '{taskList}' пуст");
                    return;
                }

                Console.WriteLine($"Задачи в списке '{taskList}':");
                Console.WriteLine(new string('-', 40));

                for (int i = 0; i < todos.Items.Count; i++)
                {
                    var todo = todos.Items[i];

                    Console.WriteLine($"{i + 1}. {todo.Text}");
                    Console.WriteLine($"   Начало: {todo.DateStart:dd.MM.yyyy HH:mm}");
                    Console.WriteLine($"   Дедлайн: {todo.DateEnd:dd.MM.yyyy HH:mm}");

                    Console.WriteLine();
                }
            }
        }

        public static void CreateToDo(string taskText, string taskList)
        {
            string fullPath = $"{Directory.GetCurrentDirectory()}\\{taskList}.json";

            using(var storage = new Storage.Storage(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                ToDoList list;

                try
                {
                    list = storage.GetToDos();
                }
                catch (FileNotFoundException)
                {
                    list = new ToDoList(taskList);
                    Console.WriteLine($"Создан новый список: {taskList}");
                }

                var newToDo = new ToDoItem(taskText)
                {
                    DateStart = DateTime.Now,
                    DateEnd = DateTime.MaxValue,
                };

                list.Add(newToDo);
                storage.SaveToDos(list);

                Console.WriteLine($"Задача добавлена в список '{taskList}' с текстом '{taskText}'");
            }           
        }

        public static void DisplayHelp()
        {
            DisplayUsage();
            Console.WriteLine();
            Console.WriteLine("Примеры:");
            Console.WriteLine("  TUIToDo -todos Работа");
            Console.WriteLine("  TUIToDo -create \"Позвонить клиенту\" Работа");
        }

        public static void DisplayUsage()
        {
            Console.WriteLine("Использование:");
            Console.WriteLine("  TUIToDo -help                     - Показать справку");
            Console.WriteLine("  TUIToDo -todos <название>         - Показать задачи из списка");
            Console.WriteLine("  TUIToDo -create <задача> <список> - Создать новую задачу");
        }
    }
}
