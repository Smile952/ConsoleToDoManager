using TUIToDo.Utils;

namespace TUIToDo.Handlers
{
    public static class CommandHandlers
    {
        public static void HandleCommand(string[] args)
        {

            string command = args[0];

            switch (command)
            {
                case "-help":
                    Commands.DisplayHelp();
                    break;

                case "-todos":
                    if (args.Length < 2)
                        throw new ArgumentException("Не указан заголовок списка");
                    HandleDisplayCommand(args);
                    break;

                case "-create":
                    if (args.Length < 3)
                        throw new ArgumentException("Недостаточно аргументов");
                    HandleCreateCommand(args);
                    break;

                default:
                    throw new ArgumentException($"Неизвестная команда: {command}");
            }
        }

        private static void HandleCreateCommand(string[] args)
        {
            var options = ArgumentParser.Parse(args);

            if (!options.TryGetValue("task", out string task))
                throw new ArgumentException();
            if (!options.TryGetValue("task_list", out string taskList))
                throw new ArgumentException();

            Commands.CreateToDo(task, taskList);
        }

        private static void HandleDisplayCommand(string[] args)
        {
            var options = ArgumentParser.Parse(args);

            if (!options.TryGetValue("task_list", out string taskList))
                throw new ArgumentException();

            Commands.DisplayToDos(taskList);
        }

    }
}
