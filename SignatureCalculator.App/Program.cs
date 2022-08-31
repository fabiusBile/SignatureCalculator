// See https://aka.ms/new-console-template for more information

using SignatureCalculator.App.ArgumentsParsing;
using SignatureCalculator.App.HashWriter;
using SignatureCalculator.App.Logging;
using SignatureCalculator.Domain;

var logger = new ConsoleLogger();
var arguments = ArgumentsParser.ParseArguments(args);
if (arguments == null)
{
    logger.LogError("Arguments are invalid");

    return;
}

if (arguments.BlockSize <= 0)
{
    logger.LogError("Block size should be greater than 0");

    return;
}

if (string.IsNullOrEmpty(arguments.FilePath))
{
    logger.LogError("File path should not be empty");

    return;
}

var signatureCalculator = new Calculator(new ConsoleHashWriter(), logger);
signatureCalculator.CalculateAndOutputSignature(arguments.FilePath, arguments.BlockSize);