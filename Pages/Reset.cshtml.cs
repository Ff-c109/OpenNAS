using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace OpenNAS.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ResetModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ResetModel> _logger;

        public ResetModel(ILogger<ResetModel> logger)
        {
            _logger = logger;
        }

        public string message { get; set; }

        public void OnGet()
        {
            if(Request.Query["passwd"] == System.IO.File.ReadAllText("Admin.conf").Split(':')[1]) {
                message = "Done";
                DirectoryInfo Files = new DirectoryInfo("wwwroot/Files");
                Files.Delete(true);
                Directory.CreateDirectory("wwwroot/Files");
                System.IO.File.Delete("ActivationCode.conf");
                System.IO.File.Delete("Admin.conf");
                System.IO.File.Delete("userInf.conf");
                System.IO.File.Delete("userPassword.conf");
            }
            else
                message = "Error: 403";
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
