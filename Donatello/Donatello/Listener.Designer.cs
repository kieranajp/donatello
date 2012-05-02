namespace Donatello
{
    partial class Listener
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Listener));
            this.txt_log = new System.Windows.Forms.TextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menu_Redownload = new System.Windows.Forms.MenuItem();
            this.menu_Logout = new System.Windows.Forms.MenuItem();
            this.menu_Exit = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // txt_log
            // 
            this.txt_log.AcceptsReturn = true;
            this.txt_log.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_log.Location = new System.Drawing.Point(9, 0);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_log.Size = new System.Drawing.Size(274, 258);
            this.txt_log.TabIndex = 0;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menu_Redownload,
            this.menu_Logout,
            this.menu_Exit});
            this.menuItem1.Text = "File";
            // 
            // menu_Redownload
            // 
            this.menu_Redownload.Index = 0;
            this.menu_Redownload.Text = "&Redownload Purchases";
            this.menu_Redownload.Click += new System.EventHandler(this.notify_redownload);
            // 
            // menu_Logout
            // 
            this.menu_Logout.Index = 1;
            this.menu_Logout.Text = "&Logout";
            this.menu_Logout.Click += new System.EventHandler(this.notify_logout);
            // 
            // menu_Exit
            // 
            this.menu_Exit.Index = 2;
            this.menu_Exit.Text = "E&xit";
            this.menu_Exit.Click += new System.EventHandler(this.notify_exit);
            // 
            // Listener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.txt_log);
            this.Menu = this.mainMenu1;
            this.Name = "Listener";
            this.Text = "Listener";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Listener_FormClosing);
            this.Resize += new System.EventHandler(this.Listener_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_log;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menu_Redownload;
        private System.Windows.Forms.MenuItem menu_Logout;
        private System.Windows.Forms.MenuItem menu_Exit;
    }
}