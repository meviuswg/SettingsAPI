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

namespace SettingsManager.Editors
{
    public partial class SettingsBooleanEditor : UserControl, ISettingValueEditor
    {
        public SettingsBooleanEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "Not a valid boolean";
            }
        }

        public string Value
        {
            get
            {
                return checkEdit1.Checked.ToString();
            }

            set
            {
                checkEdit1.Checked = bool.Parse(value);
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.Bool;
            }
        }

        public bool ValidateValue()
        {
            return true;
        }

        public bool ValidateValue(string value)
        {
            bool d;
            return bool.TryParse(value, out d);
        }
    }
}
