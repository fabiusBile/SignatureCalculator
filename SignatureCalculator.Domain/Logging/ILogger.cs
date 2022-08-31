namespace SignatureCalculator.Domain.Logging;

/// <summary>
/// Interface for the logger.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Logs the message.
    /// </summary>
    /// <param name="message">Message.</param>
    void LogError(string message);
    
    /// <summary>
    /// Logs the exception
    /// </summary>
    /// <param name="e">Exception.</param>
    /// <param name="message">Error message.</param>
    void LogError(Exception e, string message);
}