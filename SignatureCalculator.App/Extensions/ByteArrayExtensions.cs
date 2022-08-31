using System.Text;

namespace SignatureCalculator.App.Extensions;

/// <summary>
/// Extensions for <see cref="byte[]"/>
/// </summary>
public static class ByteArrayExtensions
{
    /// <summary>
    /// Converts byte array to string.
    /// </summary>
    /// <param name="bytes">Byte array.</param>
    public static string ToFormattedString(this byte[] bytes)
    {
        var stringBuilder = new StringBuilder();
        foreach (var b in bytes)
        {
            stringBuilder.Append(b.ToString("X"));
        }

        return stringBuilder.ToString();
    }
}