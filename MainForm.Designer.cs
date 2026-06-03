// =================================================================
// Author: Muhammad Bintang Bagas Prasetya
// Project: TinyDesk
// =================================================================

namespace InputController
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RadioButton radioKeyboard;
        private System.Windows.Forms.RadioButton radioTouchpad;
        private System.Windows.Forms.RadioButton radioBoth;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.RadioButton radioOff;  
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.radioKeyboard = new System.Windows.Forms.RadioButton();
            this.radioTouchpad = new System.Windows.Forms.RadioButton();
            this.radioBoth = new System.Windows.Forms.RadioButton();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.radioOff = new System.Windows.Forms.RadioButton();
            this.radioOff.Text = "OFF (Reset All)";
            this.radioOff.Location = new System.Drawing.Point(20, 15);
            this.radioOff.AutoSize = true;
            this.radioOff.Checked = true;
            this.radioOff.CheckedChanged += new System.EventHandler(this.radioOff_CheckedChanged);

            this.radioKeyboard.Text = "Disable Keyboard";
            this.radioKeyboard.Location = new System.Drawing.Point(20, 45);
            this.radioKeyboard.AutoSize = true;
            this.radioKeyboard.CheckedChanged += new System.EventHandler(this.radioKeyboard_CheckedChanged);

            this.radioTouchpad.Text = "Disable Touchpad";
            this.radioTouchpad.Location = new System.Drawing.Point(20, 75);
            this.radioTouchpad.AutoSize = true;
            this.radioTouchpad.CheckedChanged += new System.EventHandler(this.radioTouchpad_CheckedChanged);

            this.radioBoth.Text = "Disable Both";
            this.radioBoth.Location = new System.Drawing.Point(20, 105);
            this.radioBoth.AutoSize = true;
            this.radioBoth.CheckedChanged += new System.EventHandler(this.radioBoth_CheckedChanged);

            this.labelInfo.Text = "Hotkeys:\nCtrl+T: Enable Touchpad\nCtrl+K: Enable All\nCtrl+Shift+M: Show/Hide\nCtrl+Shift+C: Exit";
            this.labelInfo.Location = new System.Drawing.Point(20, 140);
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.labelInfo.ForeColor = System.Drawing.Color.DimGray;

            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ClientSize = new System.Drawing.Size(250, 240);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Controls.Add(this.radioKeyboard);
            this.Controls.Add(this.radioTouchpad);
            this.Controls.Add(this.radioBoth);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.radioOff);
            this.Text = "Tiny Desk";
            this.TopMost = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}