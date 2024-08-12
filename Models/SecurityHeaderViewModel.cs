using System.Collections.Generic;

public class SecurityHeaderViewModel
{
    public string Url { get; set; }
    public Dictionary<string, string> Headers { get; set; }
    public List<string> MissingHeaders { get; set; }
    public Dictionary<string, string> Recommendations { get; set; }
}
