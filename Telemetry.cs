namespace VariationPackCreator;

public static class Telemetry
{
    private static HttpClient _httpClient;
    private static string baseUrl = "http://telemetry.mimonsi.de:5005";
    
    static Telemetry()
    {
        _httpClient = new HttpClient();
    }
    
    public static void EnterWebsite()
    { 
        _httpClient.GetAsync(baseUrl + "/enterWebsite").ConfigureAwait(false);
    }

    
    public static void DownloadPack(string name)
    {
        _httpClient.GetAsync(baseUrl + "/downloadPack?name=" + name);
    }

    public static void StartWithTemplate(string template)
    {
        _httpClient.GetAsync(baseUrl + "/startWithTemplate?template=" + template);
    }
}