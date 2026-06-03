// =================================================================
// Author: Muhammad Bintang Bagas Prasetya
// Project: TinyDesk
// =================================================================

using System;
using System.Windows.Forms;

namespace InputController
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new SplashForm());
        }
    }
}