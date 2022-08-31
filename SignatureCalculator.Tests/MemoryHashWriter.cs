using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using SignatureCalculator.App.Extensions;
using SignatureCalculator.Domain.ResultOutput;

namespace SignatureCalculator.Tests;

/// <summary>
/// Saves the hashes to the memory.
/// </summary>
public class MemoryHashWriter : IHashWriter
{
    private readonly ConcurrentDictionary<int, string> _outputStorage = new();
    
    /// <summary>
    /// Outputs hash and block number.
    /// </summary>
    /// <param name="blockNumber">Number of the block.</param>
    /// <param name="hash">Hash of the block.</param>
    public void WriteHash(int blockNumber, byte[] hash)
    {
        _outputStorage.AddOrUpdate(blockNumber, hash.ToFormattedString(), (key, oldvalue) => oldvalue);
    }

    /// <summary>
    /// Saved hashes.
    /// </summary>
    public IReadOnlyDictionary<int, string> Output => _outputStorage;
}