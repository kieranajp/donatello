using System;
using System.Windows.Forms;

namespace Donatello
{
    /// <summary>
    /// The class for the notification pop-up.
    /// Contains methods for the management of this form.
    /// Inherits from System.Windows.Forms.Form.
    /// </summary>
    public partial class Notification : Form
    {
        #region Constructor
        /// <summary>
        /// Constructor for the class.
        /// Initialises and displays the form. Implements the visual style.
        /// Starts ticking the timer if the form is not to be permanent.
        /// </summary>
        /// <param name="txt">String: The text to be displayed on the form.</param>
        /// <param name="perma">Boolean: true if the form is to be displayed permanently on the screen. False if the form is to dispose of itself.</param>
        public Notification(string txt, bool perma)
        {
            InitializeComponent();
            this.Show();
            lblNotification.Text = txt;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Opacity = 0.7;
            if (!perma) timer.Start();
        }
        #endregion
        #region EventHandlers
        /// <summary>
        /// Kills this instance of the class and instructs the garbage collector to remove it from memory.
        /// Called when the timer ticks (2.5 second tick), or when the X button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suicide(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
