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

            SettingsStore store = new SettingsStore("_system", "_store");
            SettingsAPIClient.SettingsManager provider = new SettingsAPIClient.SettingsManager(endpoint, apiKey, store);

            var test = provider.Get();

            var image = Image.FromFile(@"C:\Users\wouter\Pictures\Saved Pictures\^15CB1E6B190158D6E857FB3116FAB429919B9B74B8FCE63A47^pimgpsh_thumbnail_win_distr.jpg");

            var result = provider.Save("test", image);
        }
    }
}