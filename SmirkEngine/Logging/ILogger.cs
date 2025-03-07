namespace SmirkEngine.Logging;

public static class Output
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
    }
    
    private static void Log(LogLevel level, string message,
        [System.Runtime.CompilerServices.CallerFilePath] string file = "",
        [System.Runtime.CompilerServices.CallerMemberName] string member = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
    {
        Console.ForegroundColor = level switch
        {
            LogLevel.Info => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => ConsoleColor.White,
        };
        System.Console.WriteLine($"{level}: {message} (File: {file}, Function: {member}, Line: {line})");
        Console.ResetColor();
    }

    public static void Log(string message)
    {
        Log(LogLevel.Info, message);
    }

    public static void LogWarning(string message)
    {
        Log(LogLevel.Warning, message);
    }

    public static void LogError(string message)
    {
        Log(LogLevel.Error, message);
    }
}