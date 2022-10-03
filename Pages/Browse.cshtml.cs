using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace OpenNAS.Pages
{
    public class BrowseModel : PageModel
    {
        public string accName { get; private set; } = string.Empty;
        public string passwd { get; private set; } = string.Empty;
        public string path {  get; private set; } = string.Empty;
        public string[] Directories { get; private set; } = new string[0];
        public string[] Files { get; private set; } = new string[0];
        public string[] DirectoriesPath { get; private set; } = new string[0];
        public string[] FilesPath { get; private set; } = new string[0];
        public string due { get; private set; } = "NO";
        public void OnGet()
        {
            accName = Request.Query["accName"];
            passwd = Request.Query["passwd"];
            path = Request.Query["path"];
            if(accName == string.Empty || accName == null)
                return;
            if (accName == "deafualt")
            {
                due = "YES";
                return;
            }
            if(path != string.Empty && path != null)
                if(path.Substring(path.Length - 3, 3) == "../")
                {
                    path = path.Substring(0, path.Length - 4);
                    string[] pathSplited = path.Split('/');
                    path = pathSplited[0];
                    for(int i = 1; i < pathSplited.Length - 1; i++)
                    {
                        path += "/" + pathSplited[i];
                    }
                }
            if(System.IO.File.Exists("userInf.conf"))
            {
                foreach(string user in System.IO.File.ReadAllLines("userInf.conf"))
                {
                    if(user == accName)
                    {
                        foreach(string inf in System.IO.File.ReadAllLines("userPassword.conf"))
                        {
                            if(inf.Split(':')[0] == accName && inf.Split(':')[1] == passwd)
                            {
                                if(path == string.Empty || path == null)
                                {
                                    Files = new string[System.IO.Directory.GetFiles("wwwroot/Files/" + accName, "*").Length];
                                    FilesPath = new string[System.IO.Directory.GetFiles("wwwroot/Files/" + accName, "*").Length];
                                    Files = System.IO.Directory.GetFiles("wwwroot/Files/" + accName, "*");
                                    FilesPath = System.IO.Directory.GetFiles("wwwroot/Files/" + accName, "*");
                                    Directories = new string[System.IO.Directory.GetDirectories("wwwroot/Files/" + accName, "*").Length];
                                    DirectoriesPath = new string[System.IO.Directory.GetDirectories("wwwroot/Files/" + accName, "*").Length];
                                    Directories = System.IO.Directory.GetDirectories("wwwroot/Files/" + accName, "*");
                                    DirectoriesPath = System.IO.Directory.GetDirectories("wwwroot/Files/" + accName, "*");
                                    for (int j = 0; j < DirectoriesPath.Length; j++)
                                    {
                                        DirectoriesPath[j] = DirectoriesPath[j].Replace("\\", "/");
                                    }
                                    for (int j = 0; j < FilesPath.Length; j++)
                                    {
                                        FilesPath[j] = FilesPath[j].Replace("\\", "/");
                                    }
                                    _ = 2233;
                                }
                                else
                                {
                                    bool acpt = false;
                                    for (int k = 0; k < path.Length; k++)
                                        for (int j = 1; j <= path.Length - k; j++)
                                        {
                                            if(path.Substring(k, j) == "wwwroot/Files/" + accName || path.Substring(k, j) == "File\\" + accName)
                                            {
                                                acpt = true;
                                            }
                                        }
                                    if(acpt)
                                    {
                                        Files = new string[System.IO.Directory.GetFiles(path, "*").Length];
                                        FilesPath = new string[System.IO.Directory.GetFiles(path, "*").Length];
                                        Files = System.IO.Directory.GetFiles(path, "*");
                                        FilesPath = System.IO.Directory.GetFiles(path, "*");
                                        Directories = new string[System.IO.Directory.GetDirectories(path, "*").Length];
                                        DirectoriesPath = new string[System.IO.Directory.GetDirectories(path, "*").Length];
                                        Directories = System.IO.Directory.GetDirectories(path, "*");
                                        DirectoriesPath = System.IO.Directory.GetDirectories(path, "*");
                                        for (int j = 0; j < DirectoriesPath.Length; j++)
                                        {
                                            DirectoriesPath[j] = DirectoriesPath[j].Replace("\\", "/");
                                        }
                                        for (int j = 0; j < FilesPath.Length; j++)
                                        {
                                            FilesPath[j] = FilesPath[j].Replace("\\", "/");
                                        }
                                    }
                                    else
                                    {
                                        Directories = new string[1];
                                        Directories[0] = "Http Error - 403 Forbidden";
                                        DirectoriesPath = new string[1];
                                        DirectoriesPath[0] = "wwwroot/Files/" + accName;
                                        return;
                                    }
                                }
                                int i = 0;
                                foreach (string Directorie in Directories)
                                {
                                    bool windowsOS = false;
                                    if (Directorie.IndexOf("\\") < 0)
                                        windowsOS = false;
                                    else
                                        windowsOS = true;
                                    if (windowsOS)
                                    {
                                        string[] SplitedEachDirectoryName = Directorie.Split('\\');
                                        Directories[i] = SplitedEachDirectoryName[SplitedEachDirectoryName.Length - 1];
                                    }
                                    else
                                    {
                                        string[] SplitedEachDirectoryName = Directorie.Split('/');
                                        Directories[i] = SplitedEachDirectoryName[SplitedEachDirectoryName.Length - 1];
                                    }
                                    i++;
                                }
                                i = 0;
                                foreach (string File in Files)
                                {
                                    bool windowsOS = false;
                                    if (File.IndexOf("\\") < 0)
                                        windowsOS = false;
                                    else
                                        windowsOS = true;
                                    if (windowsOS)
                                    {
                                        string[] SplitedEachDirectoryName = File.Split('\\');
                                        Files[i] = SplitedEachDirectoryName[SplitedEachDirectoryName.Length - 1];
                                    }
                                    else
                                    {
                                        string[] SplitedEachDirectoryName = File.Split('/');
                                        Files[i] = SplitedEachDirectoryName[SplitedEachDirectoryName.Length - 1];
                                    }
                                    i++;
                                }
                                _ = 2233;
                            }
                        }
                    }
                }
            }
        }
    }
}
