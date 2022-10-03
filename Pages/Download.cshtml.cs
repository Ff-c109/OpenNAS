using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OpenNAS.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class DownloadModel : PageModel
    {
        public string RequestId { get; set; }
        public string Data { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<DownloadModel> _logger;

        public DownloadModel(ILogger<DownloadModel> logger)
        {
            _logger = logger;
        }

        public ActionResult OnGet()
        {
            var path = Request.Query["path"].ToString().Split('/')[1];
            for(int i = 2; i < Request.Query["path"].ToString().Split('/').Length; i++) {
                path += '/' + Request.Query["path"].ToString().Split('/')[i];
            }
            return File(path, "application/octet-stream", path.Split('/')[path.Split('/').Length - 1]);//Data = System.IO.File.ReadAllText(Request.Query["path"]);
        }

        public void OnGetDelete() {
            var path = Request.Query["path"].ToString().Split('/')[1];
            for(int i = 2; i < Request.Query["path"].ToString().Split('/').Length; i++) {
                path += '/' + Request.Query["path"].ToString().Split('/')[i];
            }
            path = "wwwroot/" + path;
            System.IO.File.Delete(path);
        }
    }
}
