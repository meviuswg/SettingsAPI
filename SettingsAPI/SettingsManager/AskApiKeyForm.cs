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
    public partial class AskApiKeyForm : Form
    {
        public AskApiKeyForm(string apiKey)
        {
            InitializeComponent();

            textBox1.Text = apiKey;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string ApiKey { get { return textBox1.Text; } }
    }
}
