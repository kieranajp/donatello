using System;
using System.Windows.Forms;
using System.Threading;

namespace Donatello
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Ensure that only one instance of Donatello is running.
            bool onlyInstance = false;
            // Donatello has a unique hash, its program ID. Mutex checks this hash against other running processes.
            // If a process with the same hash is running, Donatello must be already running.
            Mutex mutex = new Mutex(true, "ae6064c155ec836e32d39685169cd272", out onlyInstance);
            if (!onlyInstance)
            {
                MessageBox.Show("An instance of Donatello is already running");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
            GC.KeepAlive(mutex);
        }
    }
}
