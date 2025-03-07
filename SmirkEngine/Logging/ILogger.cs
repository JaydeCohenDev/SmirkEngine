namespace SmirkEngine.Logging;

public static class Output
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
    }
    
    private static void Log(LogLevel level, string message, string file, string member, int line)
    {
        Console.ForegroundColor = level switch
        {
            LogLevel.Info => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => ConsoleColor.White,
        };
        var fileName = Path.GetFileNameWithoutExtension(file);
        var callerId = $"{fileName}:{member}:{line}";
        System.Console.WriteLine($"[{callerId}] [{level}] {message}");
        Console.ResetColor();
    }

    public static void Log(string message, 
        [System.Runtime.CompilerServices.CallerFilePath] string file = "",
        [System.Runtime.CompilerServices.CallerMemberName] string member = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
    {
        Log(LogLevel.Info, message, file, member, line);
    }

    public static void LogWarning(string message, 
        [System.Runtime.CompilerServices.CallerFilePath] string file = "",
        [System.Runtime.CompilerServices.CallerMemberName] string member = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
    {
        Log(LogLevel.Warning, message, file, member, line);
    }

    public static void LogError(string message, 
        [System.Runtime.CompilerServices.CallerFilePath] string file = "",
        [System.Runtime.CompilerServices.CallerMemberName] string member = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
    {
        Log(LogLevel.Error, message, file, member, line);
    }
}