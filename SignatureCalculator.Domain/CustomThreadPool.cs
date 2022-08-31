namespace SignatureCalculator.Domain;

/// <summary>
/// Custom pool of threads.
/// </summary>
public class CustomThreadPool
{
    private int _numberOfFinishedThreads;

    /// <summary>
    /// Number of threads.
    /// </summary>
    public readonly int ThreadsCount = Environment.ProcessorCount;

    /// <summary>
    /// Execute action in multiple threads.
    /// </summary>
    /// <param name="threadAction">Action to execute. Accepts thread number.</param>
    /// <param name="shouldContinue">Action should continue execution.</param>
    public void ExecuteMultithreadly(Action<int> threadAction, Func<bool> shouldContinue)
    {
        var threads = new Thread[ThreadsCount];
        for (var i = 0; i != ThreadsCount; i++)
        {
            var threadNumber = i;
            var thread = new Thread(() => ExecuteInThread(() => threadAction(threadNumber), shouldContinue));
            threads[i] = thread;
            thread.Name = $"Custom Thread Pool Thread {i}";
            thread.Start();
        }
    }

    /// <summary>
    /// All actions has finished.
    /// </summary>
    public bool HasFinished => _numberOfFinishedThreads >= ThreadsCount;

    private void ExecuteInThread(Action threadAction, Func<bool> shouldContinue)
    {
        while (shouldContinue())
        {
            threadAction();
        }

        Interlocked.Increment(ref _numberOfFinishedThreads);
    }
}