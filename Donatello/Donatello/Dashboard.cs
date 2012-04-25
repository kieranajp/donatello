using System;
using System.Configuration;
using System.Windows.Forms;
using Components;

namespace Donatello
{
    public partial class Dashboard : Form
    {
        string newUsername;
        string newPassword;

        public Dashboard()
        {
            InitializeComponent();

            notifyIcon.BalloonTipTitle = "Donatello is still running";
            notifyIcon.BalloonTipText = "Double-click to show Donatello again";
            notifyIcon.Text = "Donatello";
            notifyIcon.ContextMenu = new ContextMenu();
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Redownload Purchases", new EventHandler(notify_redownload)));
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Logout", new EventHandler(notify_logout)));
            notifyIcon.ContextMenu.MenuItems.Add(new MenuItem("Exit", new EventHandler(notify_exit)));

            bool loggedIn = String.IsNullOrEmpty(Properties.Settings.Default.LastUsername) ? false : true;

            if (!loggedIn)
            {
                pnl_Login.Show();
            }
            else
            {
                if (DbConnect.CheckLogin(Properties.Settings.Default.LastUsername, Properties.Settings.Default.LastPassword))
                {
                    WindowState = FormWindowState.Minimized;
                    this.Hide();
                    notifyIcon.BalloonTipTitle = "Donatello";
                    notifyIcon.BalloonTipText = "Server started!";
                    notifyIcon.ShowBalloonTip(2500);
                
                    Listener li = new Listener();
                    li.Show();
                }
                else
                {
                    pnl_Login.Show();
                }
            }
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();

                notifyIcon.ShowBalloonTip(2500);
                notifyIcon.Visible = true;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                notifyIcon.Visible = false;
            }
        }

        #region notifyIcon Events
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void notify_exit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void notify_redownload(object sender, EventArgs e)
        {
            Redownload r = new Redownload();
            r.Show();
        }

        private void notify_logout(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            pnl_AccountDetails.Hide();
            pnl_NewAccount.Hide();
            pnl_Login.Show();

            Properties.Settings.Default.LastUsername = null;
        }
        #endregion
        #region pnl_Login controls
        private void btn_CreateAcc_Click(object sender, EventArgs e)
        {
            pnl_Login.Hide();
            pnl_NewAccount.Show();
            Notification n = new Notification();
            n.Notify("A notification lol");
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (DbConnect.Login(emailBox.Text, passBox.Text))
            {
                WindowState = FormWindowState.Minimized;

                Properties.Settings.Default.LastUsername = emailBox.Text;
                Properties.Settings.Default.LastPassword = DbConnect.GetPassHash(emailBox.Text);

                Listener li = new Listener();
                li.Show();
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
            }
        }
        #endregion
        #region pnl_NewAccount controls
        private void btn_ExistingAccount_Click(object sender, EventArgs e)
        {
            pnl_NewAccount.Hide();
            pnl_Login.Show();
        }

        private void btn_NewAccount_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(new_emailBox.Text) || String.IsNullOrEmpty(new_passwordBox.Text) || String.IsNullOrEmpty(new_repeatPasswordBox.Text))
            {
                // TODO Error: Must fill in all fields
                // Note this will never get hit because of the placeholder values. Need to work around that.
            }
            else
            {
                // Check if email address exists
                if (String.IsNullOrEmpty(DbConnect.GetEmailAddress(new_emailBox.Text)))
                {
                    // Validate password fields
                    if (new_passwordBox.Text != new_repeatPasswordBox.Text)
                    {
                        MessageBox.Show("The two passwords are not identical!");
                    }

                    newUsername = new_emailBox.Text;
                    newPassword = new_passwordBox.Text;

                    pnl_NewAccount.Hide();
                    pnl_AccountDetails.Show();
                }
                else
                {
                    MessageBox.Show("Sorry, an account with this email address already exists!");
                }
            }
        }
        #endregion
        #region pnl_AccountDetails controls
        private void btn_Start_Click(object sender, EventArgs e)
        {
            // Sort out dates
            string newDate = new_dobBox.Value.Year + "-" + new_dobBox.Value.Month + "-" + new_dobBox.Value.Day;

            // Create account object
            Account newAccount = new Account(newUsername, new_nameBox.Text, newPassword, newDate);

            // Get it into the database
            DbConnect.CreateAccount(newAccount);

            // TODO: Send a confirmation email

            Properties.Settings.Default.LastUsername = newUsername;
            Listener li = new Listener();
            li.Show();
        }
        #endregion
    }
}
