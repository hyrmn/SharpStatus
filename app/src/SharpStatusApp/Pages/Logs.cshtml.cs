using Htmx;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SharpStatusApp.Models;

namespace SharpStatusApp.Pages;

public partial class Logs : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string RequestId { get; set; }

    public IEnumerable<Entry> Entries { get; set; }

    public void OnGet()
    {
        Entries = new List<Entry>
        {
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Get, Path = "public-api/events", Status = System.Net.HttpStatusCode.OK, Timestamp = DateTime.UtcNow.AddDays(-10), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Post, Path = "public-api/providers", Status = System.Net.HttpStatusCode.OK, Timestamp = DateTime.UtcNow.AddDays(-10), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Put, Path = "public-api/events", Status = System.Net.HttpStatusCode.OK, Timestamp = DateTime.UtcNow.AddDays(-10), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Patch, Path = "public-api/events", Status = System.Net.HttpStatusCode.OK, Timestamp = DateTime.UtcNow.AddDays(-10), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Delete, Path = "public-api/events", Status = System.Net.HttpStatusCode.OK, Timestamp = DateTime.UtcNow.AddDays(-10), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Get, Path = "public-api/providers", Status = System.Net.HttpStatusCode.BadRequest, Timestamp = DateTime.UtcNow.AddDays(-5), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Post, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadRequest, Timestamp = DateTime.UtcNow.AddDays(-5), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Put, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadRequest, Timestamp = DateTime.UtcNow.AddDays(-5), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Patch, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadRequest, Timestamp = DateTime.UtcNow.AddDays(-5), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Delete, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadRequest, Timestamp = DateTime.UtcNow.AddDays(-5), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Get, Path = "public-api/events", Status = System.Net.HttpStatusCode.NotFound, Timestamp = DateTime.UtcNow.AddDays(-2), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Post, Path = "public-api/providers", Status = System.Net.HttpStatusCode.NotFound, Timestamp = DateTime.UtcNow.AddDays(-2), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Put, Path = "public-api/events", Status = System.Net.HttpStatusCode.NotFound, Timestamp = DateTime.UtcNow.AddDays(-2), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Patch, Path = "public-api/events", Status = System.Net.HttpStatusCode.NotFound, Timestamp = DateTime.UtcNow.AddDays(-2), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Delete, Path = "public-api/events", Status = System.Net.HttpStatusCode.NotFound, Timestamp = DateTime.UtcNow.AddDays(-2), ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Get, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadGateway, Timestamp = DateTime.UtcNow.AddDays(-1), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Post, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadGateway, Timestamp = DateTime.UtcNow.AddDays(-1), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Put, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadGateway, Timestamp = DateTime.UtcNow.AddDays(-1), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Patch, Path = "public-api/providers", Status = System.Net.HttpStatusCode.BadGateway, Timestamp = DateTime.UtcNow.AddDays(-1), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Delete, Path = "public-api/events", Status = System.Net.HttpStatusCode.BadGateway, Timestamp = DateTime.UtcNow.AddDays(-1), ClientId = "36.189.22.10"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Get, Path = "public-api/providers", Status = System.Net.HttpStatusCode.NoContent, Timestamp = DateTime.UtcNow, ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Post, Path = "public-api/events", Status = System.Net.HttpStatusCode.NoContent, Timestamp = DateTime.UtcNow, ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Put, Path = "public-api/events", Status = System.Net.HttpStatusCode.NoContent, Timestamp = DateTime.UtcNow, ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Patch, Path = "public-api/events", Status = System.Net.HttpStatusCode.NoContent, Timestamp = DateTime.UtcNow, ClientId = "35.170.55.12"},
            new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Delete, Path = "public-api/providers", Status = System.Net.HttpStatusCode.NoContent, Timestamp = DateTime.UtcNow, ClientId = "35.170.55.12"},
        };
    }
}
