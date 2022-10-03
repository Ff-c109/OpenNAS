using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OpenNAS.Pages
{
    public class FirstUseModel : PageModel
    {
        public string visibility { get; private set; } = string.Empty;
        public void OnGet()
        {
            if (System.IO.File.Exists("Admin.conf")) {
                visibility = "hidden";
                return;
            }
            string accName = Request.Query["accName"];
            string passwd = Request.Query["passwd"];
            if ((accName == null || passwd == null) || accName == "deafualt")
            {
                visibility = "visible";
                return;
            }
            else
            {
                visibility = "hidden";
                System.IO.File.WriteAllText("Admin.conf", accName + ":" + passwd);
                return;
            }
        }
    }
}
