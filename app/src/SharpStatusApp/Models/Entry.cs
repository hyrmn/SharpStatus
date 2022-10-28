using System.Net;

namespace SharpStatusApp.Models;

public class Entry
{
    public string RequestId { get; set; }
    public string ProviderId { get; set; }
    public string ClientId { get; set; }
    public string Duration { get; set; }
    public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    public string Path { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public HttpMethod Method { get; set; } = HttpMethod.Get;
    public string Agent { get; set; }
    public string ForwardedFor { get; set; }
    public string Protocol { get; set; }
    public int BytesIn { get; set; }
    public int BytesOut { get; set; }
}
