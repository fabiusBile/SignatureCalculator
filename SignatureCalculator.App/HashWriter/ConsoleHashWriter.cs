using System.Text;

namespace SignatureCalculator.Domain.ResultOutput;

/// <summary>
/// Outputs hash into the console.
/// </summary>
public class ConsoleHashWriter : IHashWriter
{
    /// <summary>
    /// Outputs hash and block number into the console.
    /// </summary>
    /// <param name="blockNumber">Number of the block.</param>
    /// <param name="hash">Hash of the block.</param>
    public void WriteHash(int blockNumber, byte[] hash)
    {
        Console.WriteLine($"{blockNumber}: {ByteArrayToString(hash)}");
    }

    /// <summary>
    /// Converts byte array to string.
    /// </summary>
    /// <param name="bytes">Byte array.</param>
    private static string ByteArrayToString(IEnumerable<byte> bytes)
    {
        var stringBuilder = new StringBuilder();
        foreach (var b in bytes)
        {
            stringBuilder.Append(b.ToString("X"));
        }

        return stringBuilder.ToString();
    }
}