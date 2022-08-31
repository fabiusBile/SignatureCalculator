namespace SignatureCalculator.Domain;

/// <summary>
/// Reads file block by block.
/// </summary>
public sealed class MultiThreadBlockFileReader : IDisposable
{
    private readonly int _blockSize;
    private readonly FileStream _fileStream;
    private readonly object _locker = new();
    private int _counter = 0;
    private readonly byte[][] _buffers;
    
    public MultiThreadBlockFileReader(string filePath, int blockSize, int threadCount)
    {
        _blockSize = blockSize;
        _fileStream = File.OpenRead(filePath);
        _buffers = new byte[threadCount][];
        for (var i = 0; i != threadCount; i++)
        {
            _buffers[i] = new byte[blockSize];
        }
    }

    /// <summary>
    /// All data from the file read.
    /// </summary>
    public bool AllDataRead => _fileStream.Length - _fileStream.Position <= 0;
    
    /// <summary>
    /// Reads block from file.
    /// </summary>
    /// <param name="threadNumber">Number of the thread.</param>
    /// <returns>Number of the block, size of the block, block of data.</returns>
    public (int blockNumber, int actualBlockSize, byte[] block)? ReadBlock(int threadNumber)
    {
        lock (_locker)
        {
            var remaining = _fileStream.Length - _fileStream.Position;
            var actualBlockSize = (int) (remaining < _blockSize ? remaining : _blockSize);
            var buffer = _buffers[threadNumber];
            if (remaining <= 0)
            {
                return null;
            }
            _fileStream.Read(buffer, 0, actualBlockSize);

            return (_counter++, actualBlockSize, buffer);
        }
    }
    
    public void Dispose()
    {
        _fileStream.Dispose();
    }
}