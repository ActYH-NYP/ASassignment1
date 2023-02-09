using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace WebApplication3.Pages.Account
{
    
    public class AccessDeniedModel : PageModel
    {
        //[Authorize(Roles = "fakeadmin")]
        public void OnGet()
        {
        }
    }
}
