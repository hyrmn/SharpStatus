using Htmx;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SharpStatusApp.Models;

namespace SharpStatusApp.Pages;

public partial class LogDetails : PageModel
{
    public Entry Entry { get; set; }

    public IActionResult OnGet(string requestId)
    {
        Entry = new Entry { RequestId = Guid.NewGuid().ToString(), ProviderId = Guid.NewGuid().ToString(), Method = HttpMethod.Get, Path = "public-api/events", Status = System.Net.HttpStatusCode.OK, Timestamp = DateTime.UtcNow.AddDays(-10), ClientId = "35.170.55.12" };

        return Request.IsHtmx()
            ? Partial("_LogEntryDetails", Entry)
            : Page();

    }
}
