using TUIToDo.Handlers;
using TUIToDo.Utils;

namespace TUIToDo;

class Program
{

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Commands.DisplayUsage();
            return;
        }

        try
        {
            CommandHandlers.HandleCommand(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine("Используйте -help для справки");
        }
    }
}