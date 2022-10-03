using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace OpenNAS.Pages
{
    public class CtrlPnlModel : PageModel
    {
        public string refuse { get; private set; } = string.Empty;
        public string accName { get; private set; } = string.Empty;
        public string passwd { get; private set; } = string.Empty;
        public string[] users { get; private set; } = new string[0];
        public string actCode { get; private set; } = string.Empty;
        public string actRes { get; private set; } = "hidden";

        public void OnGet()
        {
            if (Request.Query["accName"] == System.IO.File.ReadAllText("Admin.conf").Split(':')[0] && Request.Query["passwd"] == System.IO.File.ReadAllText("Admin.conf").Split(':')[1])
            {
                refuse = "false";
            }
            else
            {
                refuse = "true";
                return;
            }
            accName = Request.Query["accName"];
            passwd = Request.Query["passwd"];
            if (Request.Query["activity"] == "delUser")
            {
                if(System.IO.File.ReadAllLines("userInf.conf").Length == 1 && System.IO.File.ReadAllLines("userInf.conf")[0] == Request.Query["userName"])
                {
                    System.IO.File.Delete("userInf.conf");
                    System.IO.File.Delete("userPassword.conf");
                    System.IO.Directory.Delete("wwwroot/Files/" + Request.Query["userName"].ToString(), true);
                    return;
                }
                string[] allUsers = System.IO.File.ReadAllLines("userInf.conf");
                string[] nowUsers = new string[System.IO.File.ReadAllLines("userInf.conf").Length - 1];
                if (true)
                {
                    int i = 0;
                    foreach (string user in allUsers)
                        if (user != Request.Query["userName"].ToString())
                            nowUsers[i++] = user;
                }
                System.IO.File.WriteAllLines("userInf.conf", nowUsers);
                string[] allPassword = System.IO.File.ReadAllLines("userPassword.conf");
                string[] nowPassword = new string[System.IO.File.ReadAllLines("userPassword.conf").Length - 1];
                if (true)
                {
                    int i = 0;
                    foreach (string password in allPassword)
                        if(Request.Query["userName"].ToString() != password.Split(':')[0])
                            nowPassword[i++] = password;
                }
                System.IO.File.WriteAllLines("userPassword.conf", nowPassword);
                System.IO.Directory.Delete("wwwroot/Files/" + Request.Query["userName"].ToString(), true);
                Console.WriteLine("wwwroot/Files/" + Request.Query["userName"].ToString());
                if (true)
                {
                    int total = System.IO.File.ReadAllLines("userInf.conf").Length;
                    foreach (string name in System.IO.File.ReadAllLines("userInf.conf"))
                        if (name == string.Empty || name == null || name == "\n")
                            total--;
                    string[] fix = new string[total];
                    int i = 0;
                    foreach (string name in System.IO.File.ReadAllLines("userInf.conf"))
                        if (name != string.Empty && name != null && name != "\n")
                            fix[i++] = name;
                    System.IO.File.Delete("userInf.conf");
                    System.IO.File.WriteAllLines("userInf.conf", fix);
                }
                if(true)
                {
                    int total = System.IO.File.ReadAllLines("userPassword.conf").Length;
                    foreach (string name in System.IO.File.ReadAllLines("userPassword.conf"))
                        if (name == string.Empty || name == null || name == "\n")
                            total--;
                    string[] fix = new string[total];
                    int i = 0;
                    foreach (string name in System.IO.File.ReadAllLines("userPassword.conf"))
                        if (name != string.Empty && name != null && name != "\n")
                            fix[i++] = name;
                    System.IO.File.Delete("userPassword.conf");
                    System.IO.File.WriteAllLines("userPassword.conf", fix);
                }
            }
            if (System.IO.File.Exists("userPassword.conf"))
            {
                users = new string[System.IO.File.ReadAllLines("userPassword.conf").Length];
                if (true)
                {
                    int i = 0;
                    foreach (string user in System.IO.File.ReadAllLines("userInf.conf"))
                    {
                        users[i] = user;
                        i++;
                    }
                }
            }
            if (Request.Query["activity"] == "makeActivationCode")
            {
                Random ran = new Random();
                string thisCode = ran.Next(1000, 9999).ToString();
                if(System.IO.File.Exists("ActivationCode.conf"))
                {
                    string[] allCode = new string[System.IO.File.ReadAllLines("ActivationCode.conf").Length];
                    while (true)
                    {
                        bool res = true;
                        foreach (string code in allCode)
                        {
                            if (thisCode == code)
                                res = false;
                        }
                        if (res)
                        {
                            break;
                        }
                        else
                        {
                            thisCode += ran.Next(1000, 9999).ToString();
                        }
                    }
                    actCode = thisCode;
                    actRes = "Visible";
                    System.IO.File.WriteAllText("ActivationCode.conf", System.IO.File.ReadAllText("ActivationCode.conf") + "\n" + thisCode);
                    int total = System.IO.File.ReadAllLines("ActivationCode.conf").Length;
                    foreach (string name in System.IO.File.ReadAllLines("ActivationCode.conf"))
                        if (name == string.Empty || name == null || name == "\n")
                            total--;
                    string[] fix = new string[total];
                    int i = 0;
                    foreach (string name in System.IO.File.ReadAllLines("ActivationCode.conf"))
                        if (name != string.Empty && name != null && name != "\n")
                            fix[i++] = name;
                    System.IO.File.Delete("ActivationCode.conf");
                    System.IO.File.WriteAllLines("ActivationCode.conf", fix);
                }
                else
                {
                    Random ran1 = new Random();
                    string thisCode1 = ran1.Next(1000, 9999).ToString();
                    actCode = thisCode1;
                    actRes = "Visible";
                    System.IO.File.WriteAllText("ActivationCode.conf", thisCode1);
                }
            }
        }
    }
}
