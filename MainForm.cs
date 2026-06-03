// =================================================================
// Author: Bintang Bagas
// Project: TinyDesk
// =================================================================

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InputController
{
    public partial class MainForm : Form
    {
        private bool keyboardDisabled = false;
        private readonly object stateLock = new object();

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private NotifyIcon trayIcon;

        public MainForm()
        {
            InitializeComponent();

            this.TopMost = true;


            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;

            trayIcon = new NotifyIcon();
            trayIcon.Text = "Tiny Desk";
            trayIcon.Visible = true;
            try {
                var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("InputController.assets.splash.png");
                Bitmap bmp = null;
                if (stream != null) {
                    bmp = new Bitmap(stream);
                } else {
                    string iconPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"assets\splash.png");
                    if (System.IO.File.Exists(iconPath)) bmp = new Bitmap(iconPath);
                }
                
                if (bmp != null) {
                    int size = Math.Min(bmp.Width, bmp.Height);
                    int x = (bmp.Width - size) / 2;
                    int y = (bmp.Height - size) / 2;
                    Bitmap sqBmp = new Bitmap(size, size);
                    using (Graphics g = Graphics.FromImage(sqBmp)) {
                        g.DrawImage(bmp, new Rectangle(0, 0, size, size), new Rectangle(x, y, size, size), GraphicsUnit.Pixel);
                    }
                    IntPtr hIcon = sqBmp.GetHicon();
                    Icon myIcon = Icon.FromHandle(hIcon);
                    this.Icon = myIcon;
                    trayIcon.Icon = myIcon;
                } else {
                    trayIcon.Icon = SystemIcons.Application;
                }
            } catch {
                trayIcon.Icon = SystemIcons.Application;
            }
            trayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;

            this.StartPosition = FormStartPosition.Manual;
            this.Load += (s, e) => {
                Rectangle workingArea = Screen.GetWorkingArea(this);
                this.Location = new Point(workingArea.Right - this.Size.Width, workingArea.Top);
            };

            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => ShowHideWindow()));
            else
                ShowHideWindow();
        }

        private void radioKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            if (radioKeyboard.Checked)
            {
                lock (stateLock)
                {
                    keyboardDisabled = true;
                }
                EnableTouchpad();
            }
        }
        private void ResetAll()
{
    EnableTouchpad();
    lock (stateLock)
    {
        keyboardDisabled = false;
    }
}
private void radioOff_CheckedChanged(object sender, EventArgs e)
{
    if (radioOff.Checked)
    {
        ResetAll();
    }
}
        private void radioTouchpad_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTouchpad.Checked)
            {
                lock (stateLock)
                {
                    keyboardDisabled = false;
                }
                DisableTouchpad();
            }
        }

        private void radioBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBoth.Checked)
            {
                lock (stateLock)
                {
                    keyboardDisabled = true;
                }
                DisableTouchpad();
            }
        }


        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(13, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                Keys key = (Keys)Marshal.ReadInt32(lParam);
                int vkCode = Marshal.ReadInt32(lParam);
                bool isCtrlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

                if (isCtrlPressed && isShiftPressed && key == Keys.C)
                {
                    if (InvokeRequired)
                        this.Invoke(new Action(() => this.Close()));
                    else
                        this.Close();
                    return (IntPtr)1;
                }

                if (isCtrlPressed && !isShiftPressed && key == Keys.T)
                {
                    EnableTouchpad();
                    lock (stateLock)
                    {
                    }
                   if (InvokeRequired)
    this.Invoke(new Action(() => radioOff.Checked = true));
else
    radioOff.Checked = true;
                    return (IntPtr)1;
                }

                if (isCtrlPressed && !isShiftPressed && key == Keys.K)
                {
                    EnableTouchpad();
                    lock (stateLock)
                    {
                        keyboardDisabled = false;
                    }
                   if (InvokeRequired)
    this.Invoke(new Action(() => radioOff.Checked = true));
else
    radioOff.Checked = true;
                    return (IntPtr)1;
                }

                if (isCtrlPressed && isShiftPressed && key == Keys.M)
                {
                    if (InvokeRequired)
                        this.Invoke(new Action(() => ShowHideWindow()));
                    else
                        ShowHideWindow();
                    return (IntPtr)1;
                }

               bool shouldBlock = false;
lock (stateLock)
{
    shouldBlock = keyboardDisabled;
}
                if (shouldBlock)
                {
                    if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.ControlKey ||
                        key == Keys.LShiftKey || key == Keys.RShiftKey || key == Keys.ShiftKey ||
                        key == Keys.LMenu || key == Keys.RMenu || key == Keys.Menu || key == Keys.Tab)
                    {
                        return CallNextHookEx(_hookID, nCode, wParam, lParam);
                    }
                    return (IntPtr)1;
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void ShowHideWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }


        private void DisableTouchpad()
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = "-Command \"Get-PnpDevice | Where-Object { $_.FriendlyName -match 'touch pad|touchpad|I2C HID' } | Disable-PnpDevice -Confirm:$false\"",
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true,
            Verb = "runas"
        });
    }
    catch { }
}
private void EnableTouchpad()
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = "-Command \"Get-PnpDevice | Where-Object { $_.FriendlyName -match 'touch pad|touchpad|I2C HID' } | Enable-PnpDevice -Confirm:$false\"",
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true,
            Verb = "runas"
        });
    }
    catch { }
}


        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk,
            int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (trayIcon != null) {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }
            UnhookWindowsHookEx(_hookID);
            EnableTouchpad();
            base.OnFormClosing(e);
            Environment.Exit(0);
        }
    }
}
