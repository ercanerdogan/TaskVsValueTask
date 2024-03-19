using BenchmarkDotNet.Running;

namespace TaskVSValueTask;

public class Program
{
    static void Main(string[] args)
    {
        var taskBenchmark = new TaskBenchmark();
        var summary = BenchmarkRunner.Run<TaskBenchmark>();

        Console.ReadKey();
    }
}