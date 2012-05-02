using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Donatello
{
    /// <summary>
    /// The class for the Redownload form
    /// Contains methods for the management of this form.
    /// Inherits from System.Windows.Forms.Form.
    /// </summary>
    public partial class Redownload : Form
    {
        #region Attributes & Constructor
        Dictionary<int, int> lookup;
        /// <summary>
        /// Constructor for the class.
        /// Initialises and displays the form. Runs the PopulateList() method.
        /// </summary>
        public Redownload()
        {
            InitializeComponent();
            PopulateList();
        }
        #endregion
        #region Methods & EventHandlers
        /// <summary>
        /// Populates the list of purchased items for this account from the database.
        /// </summary>
        private void PopulateList()
        {
            DataTable dt = DbConnect.GetAuthorisedProducts(Properties.Settings.Default.LastUsername);
            lookup = new Dictionary<int, int>();

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // A dictionary containing product_id so we can find it by the index in the checkedlistbox
                    lookup.Add(i, Convert.ToInt32(dt.Rows[i][0]));
                    purchasedItems.Items.Add(dt.Rows[i][1].ToString());
                }
                purchasedItems.Enabled = true;
                btn_Start.Enabled = true;
            }
            else
            {
                purchasedItems.Items.Add("You have not yet purchased any products!");
                purchasedItems.Enabled = false;
                btn_Start.Enabled = false;
            }
        }
        /// <summary>
        /// Called when the Start button is clicked.
        /// Instantiates a Download class to get the selected product in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (purchasedItems.SelectedIndices.Count > 0)
            {
                
                string location = DbConnect.GetLocationFromProductId(lookup[purchasedItems.SelectedIndex]);
                if (!String.IsNullOrEmpty(location))
                {
                    Download d = new Download(location);
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to download.");
            }
        }
        /// <summary>
        /// Called when the cancel button is clicked.
        /// Closes this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
    }
}
