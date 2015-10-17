using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SettingsAPIClient;

namespace SettingsManager
{
    public partial class DirectoryUserControl : UserControl
    {
        private SettingsDirectory directory;

        public DirectoryUserControl()
        {
            InitializeComponent();
        }

        
        public DirectoryUserControl(SettingsDirectory directory)
        {
            InitializeComponent();

            this.directory = directory;

            Init();
        }

        private void Init()
        {
            textApplicationName.Text = directory.Name;
            textApplicationDescription.Text = directory.Description;
        }
    }
}
