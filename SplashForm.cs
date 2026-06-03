// =================================================================
// Author: Bintang Bagas
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

            try {
                var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("InputController.assets.splash.png");
                if (stream != null) pic.Image = Image.FromStream(stream);
                else pic.Image = Image.FromFile("assets/splash.png");
            } catch { }

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