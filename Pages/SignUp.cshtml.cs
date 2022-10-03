using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OpenNAS.Pages
{
    public class SignUpModel : PageModel
    {
        public string accName { get; private set; } = string.Empty;
        public string passwd { get; private set; } = string.Empty;
        public string ActivationCode { get; private set; } = string.Empty;
        public string activeInformation { get; private set; } = "UN";
        public void OnGet()
        {
            accName = Request.Query["accName"].ToString();
            passwd = Request.Query["passwd"].ToString();
            ActivationCode = Request.Query["ActivationCode"].ToString();
            if (accName == string.Empty || passwd == string.Empty || ActivationCode == string.Empty)
            {
                return;
            }
            if (System.IO.File.Exists("ActivationCode.conf"))
            {
                string[] actCode = new string[System.IO.File.ReadAllLines("ActivationCode.conf").Length];
                actCode = System.IO.File.ReadAllLines("ActivationCode.conf");
                bool acc = false;
                foreach (string code in actCode)
                    if (ActivationCode == code)
                        acc = true;
                if (acc)
                    activeInformation = "AC";
                else
                    activeInformation = "ER";
            }
            else
                activeInformation = "ER";

            if(activeInformation == "AC")
            {
                string[] actCode = new string[System.IO.File.ReadAllLines("ActivationCode.conf").Length];
                actCode = System.IO.File.ReadAllLines("ActivationCode.conf");
                string[] usdActCode = new string[System.IO.File.ReadAllLines("ActivationCode.conf").Length - 1];
                if(true)
                {
                    int i = 0;
                    foreach (string code in actCode)
                        if (code != ActivationCode)
                        {
                            usdActCode[i] = code;
                            i++;
                        }
                }
                System.IO.File.Delete("ActivationCode.conf");
                System.IO.File.WriteAllLines("ActivationCode.conf", usdActCode);

                if(System.IO.File.Exists("userInf.conf"))
                {
                    System.IO.File.WriteAllText("userInf.conf", System.IO.File.ReadAllText("userInf.conf") + "\n" + accName);
                }
                else
                {
                    System.IO.File.WriteAllText("userInf.conf", accName);
                }
                if(System.IO.File.Exists("userPassword.conf"))
                {
                    System.IO.File.WriteAllText("userPassword.conf", System.IO.File.ReadAllText("userPassword.conf") + "\n" + accName + ':' + passwd);
                }
                else
                {
                    System.IO.File.WriteAllText("userPassword.conf", accName + ':' + passwd);
                }
                System.IO.Directory.CreateDirectory("wwwroot/Files/" + accName);
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
                if (true)
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
        }
    }
}
