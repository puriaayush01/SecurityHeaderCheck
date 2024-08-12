using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SecurityHeaderController : Controller
{
    private readonly HeaderAnalyzerService _headerAnalyzerService;

    public SecurityHeaderController(HeaderAnalyzerService headerAnalyzerService)
    {
        _headerAnalyzerService = headerAnalyzerService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CheckHeaders(string url)
    {
        var headers = await _headerAnalyzerService.GetHeadersAsync(url);
        var missingHeaders = _headerAnalyzerService.AnalyzeHeaders(headers);
        var recommendations = _headerAnalyzerService.GetRecommendations(missingHeaders);

        var model = new SecurityHeaderViewModel
        {
            Url = url,
            Headers = headers,
            MissingHeaders = missingHeaders,
            Recommendations = recommendations
        };

        return View("Results", model);
    }
}
