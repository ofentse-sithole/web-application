using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ST10104487_POE_2022.Pages
{
    public class NewModuleModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string Module_Code = Request.Form["txtModuleCode"];
            string Module_Name = Request.Form["txtModuleName"];
            double Credits = Convert.ToDouble(Request.Form["txtNumCredits"]);
            double ClassHour = Convert.ToDouble(Request.Form["txtClassHour"]);
            double NumWeeks = Convert.ToDouble(Request.Form["txtNumWeeks"]);

            ModuleFolder.Module module = new ModuleFolder.Module(Module_Code,Module_Name,Credits,ClassHour,NumWeeks);

            module.AddData();
        }
    }
}
