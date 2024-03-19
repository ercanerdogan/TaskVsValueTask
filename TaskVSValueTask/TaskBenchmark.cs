using System;
using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskVSValueTask;

[MemoryDiagnoser]
public class TaskBenchmark
{
    private readonly GitHubService _gitHubService = new GitHubService();

    [Benchmark]
    public async Task RunTask()
    {
        var names = new List<string> { "dotnet", "aspnet", "roslyn", "nuget", "xamarin" };
        for (var i = 0; i < 100_000; i++)
        {
            foreach (var name in names)
            {
                var repos = await _gitHubService.GetRepoAsyncTask(name);
            }
        }
    }

    [Benchmark]
    public async Task RunValueTask()
    {
        var names = new List<string> { "dotnet", "aspnet", "roslyn", "nuget", "xamarin" };
        for (var i = 0; i < 100_000; i++)
        {
            foreach (var name in names)
            {
                var repos = await _gitHubService.GetRepoAsyncValueTask(name);
            }
        }
    }

    [Benchmark]
    public async Task RunValueTaskTwoTimes()
    {
        var names = new List<string> { "dotnet", "aspnet", "roslyn", "nuget", "xamarin" };
        for (var i = 0; i < 100_000; i++)
        {
            foreach (var name in names)
            {
                var repos = await _gitHubService.GetRepoAsyncValueTask(name);
            }
        }
    }

}