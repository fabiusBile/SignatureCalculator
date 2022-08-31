using System.Security.Cryptography;
using SignatureCalculator.Domain.Logging;
using SignatureCalculator.Domain.ResultOutput;

namespace SignatureCalculator.Domain;

/// <summary>
/// Calculates the signature of the file and outputs to the selected writer.
/// </summary>
public class SignatureCalculator
{
    private readonly IHashWriter _hashWriter;
    private readonly ILogger _logger;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="hashWriter">Selected hash writer.</param>
    /// <param name="logger">Logger.</param>
    public SignatureCalculator(IHashWriter hashWriter, ILogger logger)
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
        }

        var buffer = new byte[blockSize];

        try
        {
            using var sha256 = SHA256.Create();
            using var stream = new FileStream(filePath, FileMode.Open);
            var bytesRead = 0;
            var counter = 0;
            while (true)
            {
                Array.Clear(buffer, 0, buffer.Length);
                bytesRead = stream.Read(buffer, bytesRead, blockSize);
                if (bytesRead == 0)
                {
                    break;
                }
                var hash = sha256.ComputeHash(buffer);
                _hashWriter.WriteHash(counter++, hash);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while processing file");
        }
    }
}