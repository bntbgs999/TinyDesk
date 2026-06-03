// =================================================================
// Author: Bintang Bagas
// Project: TinyDesk
// =================================================================

using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace InputController
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (!IsAdministrator())
            {
                // Restart program with administrator privileges
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    // The user refused the elevation
                    return;
                }
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.Run(new SplashForm());
        }

        private static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}