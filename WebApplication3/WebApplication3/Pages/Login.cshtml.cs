using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<IdentityUser> signInManager;
            
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }


        public void OnGet()
        {
        }

      

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {

                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, 
                    LModel.RememberMe, false);
                if (identityResult.Succeeded)
                {

                    var claims = new List<Claim> {
                        //new Claim(ClaimTypes.Name, "c@c.com"), 
                        //new Claim(ClaimTypes.Email,  "c@c.com"),
                        new Claim("UserID", "LG")
                    };

                    var i = new ClaimsIdentity(claims, "MyCookieAuth");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    return RedirectToPage("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password incorrect");
                }


            } //end of modelstate



            return Page();
        } //end of onpost

       

    }
}
