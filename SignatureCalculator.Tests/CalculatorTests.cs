using SignatureCalculator.Domain;
using SignatureCalculator.Domain.HashWriter;

namespace SignatureCalculator.Tests;


[TestFixture]
[TestOf(typeof(Calculator))]
public class CalculatorTests
{
    private const string TEST_FILES_DIR = "./TestData";
    
    [Test]
    [TestCaseSource(nameof(TestCases))]
    public Task CalculatedSuccessfully(string filePath, int blockSize)
    {
        var logger = new NullLogger();
        var writer = new MemoryHashWriter();
        var calculator = new Calculator(writer, logger);
        calculator.CalculateAndOutputSignature(Path.Combine(TEST_FILES_DIR, filePath), blockSize);
        return Verify(writer.Output);
    }

    private static IEnumerable<TestCaseData> TestCases()
    {
        yield return new TestCaseData("test.bin", 5);
        yield return new TestCaseData("test.bin", 1);
    }
}