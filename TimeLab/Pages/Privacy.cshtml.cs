using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TimeLab.Pages;
[Authorize(Roles = "Moderator")]
public class PrivacyModel : PageModel
{
    public void OnGet()
    {
    }
}