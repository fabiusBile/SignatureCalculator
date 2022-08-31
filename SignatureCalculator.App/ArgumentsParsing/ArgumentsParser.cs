namespace SignatureCalculator.App.ArgumentsParsing;

/// <summary>
/// Parser for the program arguments.
/// </summary>
public static class ArgumentsParser
{
    /// <summary>
    /// Creates typed arguments from the program input.
    /// </summary>
    /// <param name="args">Program input.</param>
    public static Arguments? ParseArguments(string[] args)
    {
        if (args.Length != 2)
        {
            return null;
        }

        var filePath = args[0];
        if (!int.TryParse(args[1], out var blockSize))
        {
            return null;
        }

        return new Arguments(filePath, blockSize);
    }
}