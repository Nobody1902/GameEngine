using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine;

public static class Logger
{
    public static void Log(object? message)
    {
#if DEBUG
        DateTime now = DateTime.Now;
        Console.WriteLine($"Log ({now.Hour}:{now.Minute}:{now.Second:D2}): " + message);
#endif
    }
    public static void Clear()
    {
#if DEBUG
        Console.Clear();
#endif
    }
    public static void ClearLine(int lines = 1)
    {
#if DEBUG
        for (int i = 1; i <= lines; i++)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
#endif
    }

    public static void Error(object? message)
    {
        DateTime now = DateTime.Now;
#if DEBUG
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine($"Error ({now.Hour}:{now.Minute}:{now.Second:D2}): " + message);
        Console.ForegroundColor = ConsoleColor.White;
#endif
        // Throw the error
        throw new Exception($"Error ({now.Hour}:{now.Minute}:{now.Second:D2}): " + message);
    }
    public static void Warning(object? message)
    {
#if DEBUG
        DateTime now = DateTime.Now;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Warning ({now.Hour}:{now.Minute}:{now.Second:D2}): " + message);
        Console.ForegroundColor = ConsoleColor.White;
#endif
    }
}