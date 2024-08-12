using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class HeaderAnalyzerService
{
    private readonly HttpClient _httpClient;

    public HeaderAnalyzerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Dictionary<string, string>> GetHeadersAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var headers = response.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value));
        return headers;
    }

    public List<string> AnalyzeHeaders(Dictionary<string, string> headers)
    {
        var requiredHeaders = new List<string>
        {
            "Content-Security-Policy",
            "Strict-Transport-Security",
            "X-Content-Type-Options",
            "X-Frame-Options",
            "X-XSS-Protection"
        };

        var missingHeaders = requiredHeaders.Where(header => !headers.ContainsKey(header)).ToList();
        return missingHeaders;
    }

    public Dictionary<string, string> GetRecommendations(List<string> missingHeaders)
    {
        var recommendations = new Dictionary<string, string>();

        foreach (var header in missingHeaders)
        {
            switch (header)
            {
                case "Content-Security-Policy":
                    recommendations[header] = "Implement Content Security Policy to protect against XSS attacks.";
                    break;
                case "Strict-Transport-Security":
                    recommendations[header] = "Enable Strict-Transport-Security to enforce secure connections.";
                    break;
                case "X-Content-Type-Options":
                    recommendations[header] = "Set X-Content-Type-Options to prevent MIME type sniffing.";
                    break;
                case "X-Frame-Options":
                    recommendations[header] = "Add X-Frame-Options to prevent clickjacking.";
                    break;
                case "X-XSS-Protection":
                    recommendations[header] = "Use X-XSS-Protection to enable XSS filter.";
                    break;
            }
        }

        return recommendations;
    }
}
