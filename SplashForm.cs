// =================================================================
// Author: Muhammad Bintang Bagas Prasetya
// Project: TinyDesk
// =================================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace InputController
{
    public class SplashForm : Form
    {
        Timer timer = new Timer();

        public SplashForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(400, 300);

            PictureBox pic = new PictureBox();
            pic.Dock = DockStyle.Fill;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;

            pic.Image = Image.FromFile("assets/splash.png");

            this.Controls.Add(pic);

            timer.Interval = 2000;
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Hide();
                new MainForm().Show();
            };
            timer.Start();
        }
    }
}