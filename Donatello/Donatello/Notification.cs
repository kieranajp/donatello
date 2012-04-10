using System;
//using System.Timers;
using System.Windows.Forms;

namespace Donatello
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Opacity = 0.7;
        }

        public void Notify(string txt)
        {
            lblNotification.Text = txt;
            this.Show();
            timer.Start();
        }

        public void PermaNotify(string txt)
        {
            lblNotification.Text = txt;
            this.Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
        }
    }
}
