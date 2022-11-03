using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using FTP.Base;
using System.IO;
using System.Threading;

namespace OpenNAS
{
    /*public class Server
    {
        public static FtpServer _ftpServer = null;
    }*/
    public class Program
    {
        /*static private void StartServer(string ip, string port)
        {
            if (ip == string.Empty || port == string.Empty)
            {
                throw new ArgumentException("ip or port is empty");
            }
            if (System.IO.Directory.Exists("Files") == false)
            {
                System.IO.Directory.CreateDirectory("Files");
            }
            try
            {
                Server._ftpServer = new FtpServer(ip, Convert.ToInt32(port), System.Environment.CurrentDirectory + "\\Files");
                Server._ftpServer.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static private void StopServer()
        {
            Server._ftpServer.Stop();
            Server._ftpServer = null;
        }*/

        public static void Main(string[] args)
        {
            //StartServer("127.0.0.1", "21");
            //Thread.Sleep(200);
            //string[] allUser = File.ReadAllLines("userInf.conf");
            //string[] allPasswd = File.ReadAllLines("userPassword.conf");
            /*if (true)
            {
                int i = 0;
                foreach (string user in allUser)
                {
                    Server._ftpServer.AddUser(user, allPasswd[i++].Split(':')[1]);
                }
            }*/
            //Thread.Sleep(500);
            CreateHostBuilder(args).Build().Run();
            //Thread.Sleep(500);
            //StopServer();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(File.ReadAllTexts("httpPort")).UseStartup<Startup>().UseKestrel(options =>
                    {
                        //控制中间件允许的上传文件大小为：不限制
                        options.Limits.MaxRequestBodySize = null;
                    });
                });
    }
}
