using System;
using System.Diagnostics;
using System.Net;
using System.Numerics;

namespace MalDev
{
    public class GeneralInfo
    {
        private Customization cInstance = new Customization();
        public string oSystem;
        public string uName;
        public string cDirectory;
        public string pName;
        public string ePath;
        public string tmpPath;
        public string ipv4Address;
        public string hostName;
        public int pId;
        public bool isAdmin;

        public GeneralInfo()
        {
            oSystem = Environment.OSVersion.ToString();
            uName = Environment.UserName;
            cDirectory = Environment.CurrentDirectory;
            pId = Process.GetCurrentProcess().Id;
            pName = Process.GetCurrentProcess().ProcessName;
            ePath = Process.GetCurrentProcess().MainModule.FileName;
            tmpPath = System.IO.Path.GetTempPath();
            hostName = Dns.GetHostName();
            ipv4Address = Dns.GetHostByName(hostName).AddressList[1].ToString();
            using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            isAdmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public void printInfos()
        {
            // Console.WriteLine($"This is Red{cInstance.NORMAL}");
            Console.WriteLine("**************************** SYSTEM INFOS ****************************\n");
            Console.WriteLine($"{cInstance.INFO} OPERATING SYSTEM : " + this.oSystem);
            Console.WriteLine($"{cInstance.INFO} HOSTNAME : " + this.hostName);
            Console.WriteLine($"{cInstance.INFO} USER NAME : " + this.uName);
            Console.WriteLine($"{cInstance.INFO} USER TMP PATH : " + this.tmpPath);
            Console.WriteLine($"{cInstance.INFO} IPV4 : " + this.ipv4Address);
            Console.WriteLine($"{cInstance.INFO} IS ADMIN? : " + this.isAdmin);
            Console.WriteLine("\n");
            Console.WriteLine("**************************** FILE INFO ****************************\n");
            Console.WriteLine($"{cInstance.INFO} PWD : " + this.cDirectory);
            Console.WriteLine($"{cInstance.INFO} PROCESS NAME : " + this.pName);
            Console.WriteLine($"{cInstance.INFO} FILE NAME : " + this.ePath);
            Console.WriteLine($"{cInstance.INFO} PROCESS ID : " + this.pId);
            Console.WriteLine("\n");
        }
    }
}