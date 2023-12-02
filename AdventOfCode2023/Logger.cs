using System.Text.Json;

namespace AdventOfCode2023;

public class Logger
{
    public static void Log(object message)
    {
        Console.WriteLine(message);
    }

    public static void LogJson(object message)
    {
        Console.WriteLine(JsonSerializer.Serialize(message, options: new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }
}