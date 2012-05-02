using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Donatello
{
    /// <summary>
    /// The class for the Listener form.
    /// Contains methods for the management of this form and for listening for incoming TCP messages.
    /// Inherits from System.Windows.Forms.Form.
    /// </summary>
    public partial class Listener : Form
    {
        #region Attributes & Constructor
        TcpListener serverSocket = new TcpListener(IPAddress.Any, 31337);
        TcpClient clientSocket = default(TcpClient);
        /// <summary>
        /// Constructor for this class.
        /// Initialises an instance of this class.
        /// Initialises and configures the notifyIcon and starts running a background thread.
        /// </summary>
        public Listener()
        {
            InitializeComponent();

            notifyIcon.Text = "Donatello";
            notifyIcon.ContextMenu = new ContextMenu();
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Redownload Purchases", new EventHandler(notify_redownload)));
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Logout", new EventHandler(notify_logout)));
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", new EventHandler(notify_exit)));

            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerAsync();
        }
        #endregion
        #region notifyIcon EventHandlers
        /// <summary>
        /// Called when the form is resized.
        /// If the form is minimised, moves the form to the notification area rather than the taskbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Listener_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon.BalloonTipTitle = "Donatello is still running";
                notifyIcon.BalloonTipText = "Right-click this icon for controls";
                notifyIcon.ShowBalloonTip(2500);
                // TODO: This is behaving weirdly.
                // this.ShowInTaskbar = false;
                // this.Hide(); 
            }
        }
        /// <summary>
        /// Called when the notifyIcon is double-clicked.
        /// Shows the form again after it has been minimised to the taskbar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //this.ShowInTaskbar = true;
            //this.ShowDialog();
        }
        /// <summary>
        /// Called when the Exit button on the notifyIcon's right-click menu is clicked.
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_exit(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
            notifyIcon.Visible = false;
            Application.Exit();
        }
        /// <summary>
        /// Called when the Redownload button on the notifyIcon's right-click menu is clicked.
        /// Opens an instance of the Redownload form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_redownload(object sender, EventArgs e)
        {
            Redownload r = new Redownload();
            r.ShowDialog();
        }
        /// <summary>
        /// Called when the Logout button on the notifyIcon's right-click menu is clicked.
        /// Logs out the user, cancels the Listener and restarts the application, effectively returning the user to the login form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notify_logout(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastUsername = null;
            Properties.Settings.Default.LastPassword = null;
            Properties.Settings.Default.Save();
            backgroundWorker.CancelAsync();
            this.Hide();
            this.Dispose();
            Application.Restart();
            // TODO: Restart application
        }
        #endregion
        #region Delegate functions
        /// <summary>
        /// A delegate function to allow writing to the log textbox from other threads.
        /// </summary>
        /// <param name="add">String: The text to write to the log</param>
        private delegate void WriteLogDelegate(string add);
        private void WriteLog(string add)
        {
            if (this.txt_log.InvokeRequired)
            {
                this.txt_log.Invoke(new WriteLogDelegate(this.WriteLog), add);
            }
            else
            {
                this.txt_log.Text += add + Environment.NewLine;
            }
        }
        /// <summary>
        /// A delegate function to allow invocation of Notification objects from other threads.
        /// </summary>
        /// <param name="txt">String: The text to display on the Notification.</param>
        private delegate void NotifyDelegate(string txt);
        private void Notify(string txt)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new NotifyDelegate(this.Notify), txt);
            }
            else
            {
                Notification n = new Notification(txt, false);
            }
        }
        /// <summary>
        /// A delegate function to allow showing and hiding of this form from other threads.
        /// </summary>
        /// <param name="show">Boolean: Whether to show or hide the form.</param>
        private delegate void AppearDelegate(bool show);
        private void Appear(bool show)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AppearDelegate(this.Appear), show);
            }
            else
            {
                if (show)
                    this.Hide();
                else
                    this.Show();
            }
        }
        #endregion
        #region EventHandlers
        /// <summary>
        /// Called when the form is closed.
        /// Stops the background thread and exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Listener_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker.CancelAsync();
            Application.Exit();
        }
        /// <summary>
        /// Main loop for the background worker.
        /// Starts a TCP server and constantly attempts to accept connections from clients.
        /// When a connection is made, spawns an instance of ClientHandler to deal with communication.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            int counter = 0;
            WriteLog("Starting server...");
            serverSocket.Start();
            System.Threading.Thread.Sleep(500);
            WriteLog("Server started!");
            Appear(false);
            Notify("Server started!");

            while (true)
            {
                counter++;
                clientSocket = serverSocket.AcceptTcpClient();
                WriteLog("Connection " + counter.ToString() + " made!");
                Notify("Connection " + counter.ToString() + " made!");
                ClientHandler ch = new ClientHandler(clientSocket);
            }
        }
        #endregion
    }
}
