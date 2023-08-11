using System.Text;

namespace AccountManagingServer;

public static class HttpRequests
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> GetRequestAsync(string url)
    {
        using HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    public static async Task<string> PostRequestAsync(string url, string json)
    {
        using HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await client.PostAsync(url, content).ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    public static async Task<string> PatchRequestAsync(string url, string json)
    {
        using HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        using HttpResponseMessage response = await client.PatchAsync(url, content).ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    public static async Task<string> DeleteRequestAsync(string url)
    {
        using HttpResponseMessage response = await client.DeleteAsync(url).ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}
