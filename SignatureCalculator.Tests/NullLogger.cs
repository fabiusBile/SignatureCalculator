
using SignatureCalculator.Domain.Logging;

namespace SignatureCalculator.Tests;

/// <summary>
/// Logs nothing.
/// </summary>
public class NullLogger : ILogger
{
    /// <summary>
    /// Logs the message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void LogError(string message) { }

    /// <summary>
    /// Logs the exception
    /// </summary>
    /// <param name="e">Exception.</param>
    /// <param name="message">Error message.</param>
    public void LogError(Exception e, string message) { }
}