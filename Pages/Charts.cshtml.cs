using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ST10104487_POE_2022.Pages
{
    public class ChartsModel : PageModel
    {
        public List<ModuleFolder.Module> modulelist = new List<ModuleFolder.Module>();
        public void OnGet()
        {
            ModuleFolder.Module module = new ModuleFolder.Module();
            modulelist = module.allModule();
        }
    }
}
