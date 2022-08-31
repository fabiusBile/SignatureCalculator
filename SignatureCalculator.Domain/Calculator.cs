using System.Security.Cryptography;
using SignatureCalculator.Domain.HashWriter;
using SignatureCalculator.Domain.Logging;

namespace SignatureCalculator.Domain;

/// <summary>
/// Calculates the signature of the file and outputs to the selected writer.
/// </summary>
public class Calculator
{
    private readonly IHashWriter _hashWriter;
    private readonly ILogger _logger;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="hashWriter">Selected hash writer.</param>
    /// <param name="logger">Logger.</param>
    public Calculator(IHashWriter hashWriter, ILogger logger)
    {
        _hashWriter = hashWriter;
        _logger = logger;
    }

    /// <summary>
    /// Calculates the signature of the file and outputs to the selected writer.
    /// </summary>
    /// <param name="filePath">Path to the file for calculating the signature.</param>
    /// <param name="blockSize">Size of the block for hashing.</param>
    public void CalculateAndOutputSignature(string filePath, int blockSize)
    {
        if (!File.Exists(filePath))
        {
            _logger.LogError($"File {filePath} doesn't exist");
            
            return;
        }

        try
        {
            var customThreadPool = new CustomThreadPool();
            using var reader = new MultiThreadBlockFileReader(filePath, blockSize, customThreadPool.ThreadsCount);
            customThreadPool
                .ExecuteMultithreadly(threadNumber => { ReadAndProcessBlock(reader, threadNumber); }
                    , () => !reader.AllDataRead);

            while (!customThreadPool.HasFinished)
            {
                Thread.Sleep(500);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while processing file");
        }
    }

    private void ReadAndProcessBlock(MultiThreadBlockFileReader reader, int threadNumber)
    {
        using var sha256 = SHA256.Create();
        var result = reader.ReadBlock(threadNumber);
        if (result == null)
        {
            return;
        }

        var (blockNumber, actualBlockSize, buffer) = result.Value;
        var hash = sha256.ComputeHash(buffer, 0, actualBlockSize);
        _hashWriter.WriteHash(blockNumber, hash);
    }
}