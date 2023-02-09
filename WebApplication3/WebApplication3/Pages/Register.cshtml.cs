using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;



namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }
        private readonly RoleManager<IdentityRole> roleManager;
        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }



        public void OnGet()
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection fc)
        {
            string res = fc["FirstName"];
            string res2 = fc["LastName"];
            string res3 = fc["Gender"];
            string res4 = fc["NRIC"];
            string res5 = fc["Email"];
            string res6 = fc["Password"];
            string res7 = fc["ConfirmPassword"];
            string res8 = fc["DateOfBirth"];
            string res9 = fc["AboutMe"];

            return Page();

        }

        public IActionResult Index()
        {
            return Page();
        }


        //saves to db
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                //makes new user and puts into db
                var user = new IdentityUser()
                {
                    UserName = RModel.FirstName,
                    Email = RModel.Email

                };

                //Create the Admin role if NOT exist
                IdentityRole role = await roleManager.FindByIdAsync("Admin"); if (role == null)
                {
                    IdentityResult result2 = await roleManager.CreateAsync(new IdentityRole("Admin")); if (!result2.Succeeded)
                    {
                        ModelState.AddModelError("", "Create role admin failed");
                    }
                }


                var result = await userManager.CreateAsync(user, RModel.Password);
                
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }







    }
}
