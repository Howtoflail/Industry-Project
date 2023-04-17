/// <summary>
/// Creates an absolute URL prefixed by a base URL + version tag.
/// </summary>
public static class APIUrl
{
    private static string baseUrl = "http://127.0.0.1:8080/api";

    /// <summary>
    /// Creates an URL for version 1 of the API.
    /// </summary>
    public static string CreateV1(string url)
    {
        return $"{baseUrl}/v1{url}";
    }
}
