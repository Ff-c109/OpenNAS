using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenNAS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; private set; } = " ";
        public string[] Dirs { get; private set; } = new string[12];
        public string[] Files { get; private set; } = new string[12];
        public string accName { get; private set; } = string.Empty;
        public string passwd { get; private set; } = string.Empty;
        public string ConfigurationButton { get; private set; } = string.Empty;
        public string visibility { get; private set; } = string.Empty;
        public string redirect { get; private set; } = "NO";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            /*try
            {
                string admin = System.IO.File.ReadAllText("Admin.conf");
                if (admin == string.Empty)
                {
                    Message = "Configuration";
                    ConfigurationButton = "width: 6em; height: 2em";
                    return;
                }
            }
            catch (Exception ex)
            {
                Message = "Configuration";
                ConfigurationButton = "width: 6em; height: 2em";
                return;
            }*/
            if(System.IO.File.Exists("Admin.conf"))
            {
                if(Request.Query["accName"] == System.IO.File.ReadAllText("Admin.conf").Split(':')[0] && Request.Query["passwd"] == System.IO.File.ReadAllText("Admin.conf").Split(':')[1])
                {
                    Message = "Welcome Administrator";
                    ConfigurationButton = "width: 6em; height: 0px; visibility: hidden";
                    accName = Request.Query["accName"];
                    passwd = Request.Query["passwd"];
                    return;
                }
                else
                {
                    Message = "Http Error - 403 Forbidden";
                    ConfigurationButton = "width: 6em; height: 0px; visibility: hidden";
                    visibility = "hidden";
                    if (Request.Query["accName"].ToString() == null || Request.Query["accName"].ToString() == string.Empty)
                        redirect = "NO";
                    else
                    {
                        redirect = "YES";
                        Message = "File Explorer";
                    }
                    accName = Request.Query["accName"];
                    passwd = Request.Query["passwd"];
                    return;
                }
            }
            else
            {
                Message = "Configuration";
                ConfigurationButton = "width: 18em; height: 2em";
                visibility = "hidden";
                return;
            }
        }
    }
}
