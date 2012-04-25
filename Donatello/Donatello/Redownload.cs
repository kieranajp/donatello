using System;
using System.Data;
using System.Windows.Forms;
using Components;

namespace Donatello
{
    public partial class Redownload : Form
    {
        public Redownload()
        {
            InitializeComponent();
            PopulateList();
        }

        private void PopulateList()
        {
            // TODO: YOU ARE HERE
            DataTable dt = DbConnect.GetAuthorisedProducts(Properties.Settings.Default.LastUsername);

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    purchasedList.Items.Add(dt.Rows[i]);
                }
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            foreach (object ci in purchasedList.CheckedItems)
            {
                DataRowView castedItem = (DataRowView)ci;
                
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
