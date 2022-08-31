using SignatureCalculator.Domain.Logging;

namespace SignatureCalculator.App.Logging;

/// <summary>
/// Writes logs into the console.
/// </summary>
public class ConsoleLogger : ILogger
{
    /// <summary>
    /// Logs the error message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void LogError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    /// <summary>
    /// Logs the exception and the error message.
    /// </summary>
    /// <param name="e">Exception.</param>
    /// <param name="message">Error message.</param>
    public void LogError(Exception e, string message)
    {
        LogError($"Message: {message}\nException: {e}");
    }
}