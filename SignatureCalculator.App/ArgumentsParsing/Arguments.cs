namespace SignatureCalculator.App.ArgumentsParsing;

/// <summary>
/// Typed arguments for the program.
/// </summary>
/// <param name="FilePath">Path to the file.</param>
/// <param name="BlockSize">Size of the block.</param>
public record Arguments(string FilePath, int BlockSize);