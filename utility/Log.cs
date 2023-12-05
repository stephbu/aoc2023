namespace Utility;

public class Log
{
    public static int Level = 3;

    public static void Warning(string message)
    {
        if (Level >= 2)
        {
            Log.LogMessage("W", message);
        }
    }
    
    public static void Info(string message)
    {
        if(Level >= 3)
        {
            Log.LogMessage("I", message);
        }
    }
    
    public static void Error(Exception exception)
    {
        if(Level >= 1) 
        {
            Log.LogMessage("E", exception.Message);
        }
    }

    public static void Line(string message)
    {
        Log.LogMessage("L", message);
    }
    
    private static void LogMessage(string tag, string message)
    {
        Console.WriteLine($"[{DateTime.Now}] {tag} {message}");
    }
}