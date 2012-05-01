using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.Mail;

namespace Donatello
{
    /// <summary>
    /// The class for the Dashboard form.
    /// Contains methods for the management of this form.
    /// Inherits from System.Windows.Forms.Form.
    /// </summary>
    public partial class Dashboard : Form
    {
        #region Attributes & Constructor
        string newUsername;
        string newPassword;
        /// <summary>
        /// Constructor for this class. 
        /// Initialises and displays the form.
        /// Checks if valid user credentials are stored for automatic login. If so, closes this form and opens an instance of Listener.
        /// Else, shows the login panel.
        /// </summary>
        public Dashboard()
        {
            InitializeComponent();
            this.Visible = false;

            if (Properties.Settings.Default.LastUsername != "email@address.com" && DbConnect.CheckLogin(Properties.Settings.Default.LastUsername, Properties.Settings.Default.LastPassword))
            {
                Account acc = DbConnect.GetAccount(Properties.Settings.Default.LastUsername);
                Notification n = new Notification("Welcome, " + acc.Name + "!", false);

                this.Hide();
                DbConnect.SetClient(Properties.Settings.Default.LastUsername);
                Listener li = new Listener();
                li.ShowDialog();
            }
            else
            {
                this.Visible = true;
                pnl_Login.Show();
            }
        }
        #endregion
        #region pnl_Login controls
        /// <summary>
        /// Hides the login panel and opens the Create Account panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreateAcc_Click(object sender, EventArgs e)
        {
            pnl_Login.Hide();
            pnl_NewAccount.Show();
        }
        /// <summary>
        /// Attempts to log in using the input credentials. If successful, hides this form and creates an instance of Listener.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(emailBox.Text) || String.IsNullOrEmpty(passBox.Text) || emailBox.Text == "email@address.com")
            {
                MessageBox.Show("You must fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (DbConnect.Login(emailBox.Text, passBox.Text))
            {
                DbConnect.SetClient(emailBox.Text);
                WindowState = FormWindowState.Minimized;

                Properties.Settings.Default.LastUsername = emailBox.Text;
                Properties.Settings.Default.LastPassword = DbConnect.GetPassHash(emailBox.Text);
                Properties.Settings.Default.Save();

                Account acc = DbConnect.GetAccount(emailBox.Text);
                Notification n = new Notification("Welcome, " + acc.Name + "!", false);

                Listener li = new Listener();
                li.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect username or password!");
            }
        }
        #endregion
        #region pnl_NewAccount controls
        /// <summary>
        /// Hides the New Account panel and opens the login panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ExistingAccount_Click(object sender, EventArgs e)
        {
            pnl_NewAccount.Hide();
            pnl_Login.Show();
        }
        /// <summary>
        /// Checks the user's input and, if valid, hides this panel and shows the Account Details panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NewAccount_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(new_emailBox.Text) || String.IsNullOrEmpty(new_passwordBox.Text)
                || new_emailBox.Text == "email@address.com" || new_repeatPasswordBox.Text == "passward")
            {
                MessageBox.Show("You must fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Check for valid email address
                MailMessage mail = new MailMessage();
                try
                {
                    mail.To.Add(new_emailBox.Text);
                }
                catch (FormatException)
                {
                    // If the email address is invalid this exception will be thrown.
                    MessageBox.Show("You entered an invalid email address", "Email address error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Check if email address exists
                if (!DbConnect.AccountExists(new_emailBox.Text))
                {
                    // Validate password fields
                    if (new_passwordBox.Text != new_repeatPasswordBox.Text)
                    {
                        MessageBox.Show("The two passwords are not identical!", "Password error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    newUsername = new_emailBox.Text;
                    newPassword = new_passwordBox.Text;

                    pnl_NewAccount.Hide();
                    pnl_AccountDetails.Show();
                }
                else
                {
                    MessageBox.Show("Sorry, an account with this email address already exists!", "User error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
        #region pnl_AccountDetails controls
        /// <summary>
        /// Verifies the user's input and if valid closes this form and starts an instance of Listener.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Start_Click(object sender, EventArgs e)
        {
            // Sort out dates
            string newDate = new_dobBox.Value.Year + "-" + new_dobBox.Value.Month + "-" + new_dobBox.Value.Day;

            // Create account object
            Account newAccount = new Account(newUsername, new_nameBox.Text, newPassword, newDate);

            // Get it into the database
            DbConnect.CreateAccount(newAccount);

            // Send a confirmation email message
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.kajp.im");
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential("donatello+kajp.im", "eijonu");
            smtp.EnableSsl = false;

            mail.From = new MailAddress("donatello@kajp.im");
            mail.To.Add(newUsername);
            mail.Subject = newAccount.Name + ", welcome to Donatello!";
            mail.Body = "Thanks for joining Donatello, " + newAccount.Name + "!";

            try
            {
                //smtp.Send(mail);
            }
            catch (SmtpException) 
            { 
                // In case the SMTP server is down. In which case, it's not the end of the world if the email doesn't send.
            }

            Properties.Settings.Default.LastUsername = newUsername;
            Properties.Settings.Default.Save();
            DbConnect.SetClient(newUsername);
            Listener li = new Listener();
            li.ShowDialog();
            this.Hide();
        }
        #endregion
    }
}
