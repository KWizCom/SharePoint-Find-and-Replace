using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            this.Text = "About " + Constants.ProductName;
            labelVersion.Text = Constants.ProductVersion;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
