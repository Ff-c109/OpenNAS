using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OpenNAS.Pages
{
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //[ValidateAntiForgeryToken]
    public class UploadModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<UploadModel> _logger;

        public string State { get; set; }

        public UploadModel(ILogger<UploadModel> logger)
        {
            _logger = logger;
        }

        public void OnGet() {
            State = " ";
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task OnPostAsync()
        {
            var accName = Request.Query["accName"].ToString();
            var passwd = Request.Query["passwd"].ToString();
            var path = Request.Query["path"].ToString();
            
            if(System.IO.File.Exists("userInf.conf")) {
                foreach(string user in System.IO.File.ReadAllLines("userInf.conf")) {
                    if(user == accName) {
                        foreach(string inf in System.IO.File.ReadAllLines("userPassword.conf")) {
                            if(inf.Split(':')[0] == accName && inf.Split(':')[1] == passwd) {
                                if(path == string.Empty || path == null || path == "/") {
                                    Console.WriteLine(Upload.FileName);
                                    using (var fileStream = new FileStream("wwwroot/Files/" + accName + '/' + Upload.FileName, FileMode.Create))
                                    {   
                                        await Upload.CopyToAsync(fileStream);
                                        Console.WriteLine(Upload.FileName);
                                    }
                                    State = "Done.";
                                }
                                else {
                                    bool acpt = false;
                                    for (int k = 0; k < path.Length; k++)
                                        for (int j = 1; j <= path.Length - k; j++)
                                        {
                                            if(path.Substring(k, j) == "wwwroot/Files/" + accName || path.Substring(k, j) == "File\\" + accName)
                                            {
                                                acpt = true;
                                                continue;
                                            }
                                        }
                                    if(acpt)
                                    {
                                        Console.WriteLine(Upload.FileName);
                                        using (var fileStream = new FileStream(path + Upload.FileName, FileMode.Create))
                                        {
                                            await Upload.CopyToAsync(fileStream);
                                            Console.WriteLine(Upload.FileName);
                                        }
                                        State = "Done.";
                                    }
                                    else
                                    {
                                        State = "Http Error - 403 Forbidden";
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
