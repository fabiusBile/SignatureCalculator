using System.Text;
using SignatureCalculator.App.Extensions;
using SignatureCalculator.Domain.ResultOutput;

namespace SignatureCalculator.App.HashWriter;

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
        Console.WriteLine($"{blockNumber}: {hash.ToFormattedString()}");
    }
}