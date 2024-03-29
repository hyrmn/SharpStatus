using Htmx;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

namespace SharpStatusApp.Pages;

public class Index : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if(Input.Password == "fail")
        {
            ModelState.AddModelError("", "Incorrect username or password");
            return Request.IsHtmx()
                ? Partial("_LoginForm", this.Input)
                : Page();
        }

        if (Request.IsHtmx())
        {
            Response.Htmx(headers => headers.Redirect(Url.Page("Logs") ?? "logs"));
            return this.Content("");
        }

        return Redirect("~/logs");
    }
}
