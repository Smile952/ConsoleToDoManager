using TUIToDo.Models;
using TUIToDo.Utils;

namespace TUIToDo;

class Program
{
    private static Storage _storage;

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            DisplayUsage();
            return;
        }

        _storage = new Storage(Directory.GetCurrentDirectory());

        try
        {
            ProcessCommand(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine("Используйте -help для справки");
        }
    }

    private static void ProcessCommand(string[] args)
    {
        string command = args[0];

        switch (command)
        {
            case "-help":
                DisplayHelp();
                break;

            case "-todos":
                if (args.Length < 2)
                    throw new ArgumentException("Не указан заголовок списка");
                DisplayToDos(args[1]);
                break;

            case "-create":
                if (args.Length < 3)
                    throw new ArgumentException("Недостаточно аргументов");
                CreateToDo(args[1], args[2]);
                break;

            default:
                throw new ArgumentException($"Неизвестная команда: {command}");
        }

    }
    private static void DisplayToDos(string title)
    {
        var todos = _storage.GetToDosByTitle(title);

        if (todos.Items.Count == 0)
        {
            Console.WriteLine($"Список '{title}' пуст");
            return;
        }

        Console.WriteLine($"Задачи в списке '{title}':");
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

    private static void CreateToDo(string taskText, string listTitle)
    {
        ToDoList list;

        try
        {
            list = _storage.GetToDosByTitle(listTitle);
        }
        catch (FileNotFoundException)
        {
            list = new ToDoList(listTitle);
            Console.WriteLine($"Создан новый список: {listTitle}");
        }

        var newToDo = new ToDoItem(taskText)
        {
            DateStart = DateTime.Now,
            DateEnd = DateTime.MaxValue,
        };

        list.Add(newToDo);
        _storage.SaveToDos(list);

        Console.WriteLine($"Задача добавлена в список '{listTitle}':");
        Console.WriteLine($"  ✓ {taskText}");
    }

    private static void DisplayHelp()
    {
        DisplayUsage();
        Console.WriteLine();
        Console.WriteLine("Примеры:");
        Console.WriteLine("  TUIToDo -todos Работа");
        Console.WriteLine("  TUIToDo -create \"Позвонить клиенту\" Работа");
    }

    private static void DisplayUsage()
    {
        Console.WriteLine("Использование:");
        Console.WriteLine("  TUIToDo -help                    - Показать справку");
        Console.WriteLine("  TUIToDo -list                    - Список всех ToDo листов");
        Console.WriteLine("  TUIToDo -todos <название>       - Показать задачи из списка");
        Console.WriteLine("  TUIToDo -create <задача> <список> - Создать новую задачу");
    }
}