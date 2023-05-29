using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ST10104487_POE_2022.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string Username = Request.Form["txtUsername"];
            string Password = Request.Form["txtPassword"];
            string Confirm_Password = Request.Form["txtConfirmPassword"];

            ModuleFolder.Register reg = new ModuleFolder.Register(Username,Password,Confirm_Password);

            //Adds registration data in the database 
            reg.AddRegisterData();

        }
    }
}
