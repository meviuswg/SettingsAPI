using SettingsAPIClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string endpoint = "http://localhost:5945/api";
            string apiKey = "a33a5f531f49480eac31d64d02163bcf";

      
            SettingsAPIClient.SettingsManager provider = new SettingsAPIClient.SettingsManager(endpoint, apiKey);
  
        }
    }
}