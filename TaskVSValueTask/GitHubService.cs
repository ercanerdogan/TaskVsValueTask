using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskVSValueTask;

public class GitHubService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;

    public GitHubService()
    {
        _httpClient = new HttpClient();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _httpClient.BaseAddress = new Uri("https://api.github.com/");
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("TaskVSValueTask", "1.0"));
    }

    public async Task<List<Repo>> GetRepoAsyncTask(string userName)
    {
        var cacheKey = $"repos_{userName}";
        if (_memoryCache.TryGetValue(cacheKey, out List<Repo> repos))
        {
            return repos;
        }

        var response = await _httpClient.GetStringAsync($"users/{userName}/repos");
        repos = JsonSerializer.Deserialize<List<Repo>>(response);

        _memoryCache.Set(cacheKey, repos, TimeSpan.FromHours(1));

        return repos;
    }

    public async ValueTask<List<Repo>> GetRepoAsyncValueTask(string userName)
    {
        var cacheKey = $"repos_{userName}";
        if (_memoryCache.TryGetValue(cacheKey, out List<Repo> repos))
        {
            return repos;
        }

        var response = await _httpClient.GetStringAsync($"users/{userName}/repos");
        repos = JsonSerializer.Deserialize<List<Repo>>(response);

        _memoryCache.Set(cacheKey, repos, TimeSpan.FromHours(1));

        return repos;
    }
}

public class Repo
{
}