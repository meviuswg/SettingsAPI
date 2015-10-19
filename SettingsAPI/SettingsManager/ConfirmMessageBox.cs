using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class ConfirmMessageBox : Form
    {
        public static DialogResult Show(string confirmationMessage)
        {
            return new ConfirmMessageBox(confirmationMessage).ShowDialog();
        }

        public ConfirmMessageBox(string confirmationMessage)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;

            labelText.Text = confirmationMessage;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = checkBoxIAmReallyReallyReallySure.Checked;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if(checkBoxIAmReallyReallyReallySure.Checked)
            {
                DialogResult = DialogResult.OK;
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();

        }
    }
}
