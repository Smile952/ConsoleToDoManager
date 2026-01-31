namespace TUIToDo.Utils
{
    public static class ArgumentParser
    {

        public static Dictionary<string, string> Parse(string[] args, int startIndex = 1)
        {
            var result = new Dictionary<string, string>();

            for (int i = startIndex; i < args.Length; i++)
            {
                if (!args[i].StartsWith("--"))
                {
                    throw new ArgumentException();
                }
                
                string key = args[i][2..];

                if (i+1 >= args.Length || args[i+1].StartsWith("--"))
                    throw new ArgumentException();

                result[key] = args[i+1];
                i++;
            }
            return result;
        }

    }
}
