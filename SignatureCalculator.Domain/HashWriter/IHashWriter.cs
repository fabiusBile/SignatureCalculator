namespace SignatureCalculator.Domain.HashWriter;

/// <summary>
/// Outputs the result of hash calculation.
/// </summary>
public interface IHashWriter
{
    /// <summary>
    /// Outputs hash and block number.
    /// </summary>
    /// <param name="blockNumber">Number of the block.</param>
    /// <param name="hash">Hash of the block.</param>
    void WriteHash(int blockNumber, byte[] hash);
}