using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ST10104487_POE_2022.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string username = Request.Form["txtUsername"];
            string password = Request.Form["txtPassword"];

            ModuleFolder.Register reg = new ModuleFolder.Register();
            reg.getRegister(username);

            if (reg != null)
            {
                if (reg.Username.Equals(username) && reg.Password.Equals(password))
                {
                    Response.Redirect("/AllModules");
                }
            }
            else
            {
                Response.Redirect("/AllModules");
            }

        }
    }
}