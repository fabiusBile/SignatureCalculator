using System.Security.Cryptography;
using SignatureCalculator.Domain.Logging;
using SignatureCalculator.Domain.ResultOutput;

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

        var buffer = new byte[blockSize];

        try
        {
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            
            var counter = 0;
            while (true)
            {
                Array.Clear(buffer, 0, buffer.Length);
                var remaining = stream.Length - stream.Position;
                var actualBlockSize = (int) (remaining < blockSize ? remaining : blockSize);
                if (remaining <= 0)
                {
                    return;
                }
                stream.Read(buffer, 0, actualBlockSize);
                var hash = sha256.ComputeHash(buffer, 0, actualBlockSize);
                _hashWriter.WriteHash(counter++, hash);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while processing file");
        }
    }
}