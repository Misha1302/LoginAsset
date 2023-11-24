using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public sealed class KeysManager : SingletonMb<KeysManager>
{
    [Tooltip("Создайте текстовый файл в google drive, опубликуйте его и вставьте сюда ссылку")]
    [SerializeField] private string url =
        "https://docs.google.com/document/d/e/2PACX-1vS-QKMo_JEl3XsmjRoPUAfem_4MttFK-p9hbDeVskereWxXRyFwAIxbQ0UlcDNZ8OCNA99vZxFmaXaQ/pub";

    public IReadOnlyList<ReadOnlySecretKey> Keys { get; private set; }

    protected override async void Awake()
    {
        base.Awake();

        var arr = ToArrayOfStrings(await GetContext());
        var keys = arr.Select(x => new ReadOnlySecretKey(x));
        Keys = keys.ToList();
        print(string.Join("\n", Keys));
    }

    private static string[] ToArrayOfStrings(string responseBody)
    {
        var message = Regex.Match(responseBody, "<div class=\"c. doc-content\">(.|\n)*</div>").Value;
        var context = Regex.Replace(message, "<[^<>]*>", "\n");
        return context.Split("\n", StringSplitOptions.RemoveEmptyEntries);
    }

    private async Task<string> GetContext()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}