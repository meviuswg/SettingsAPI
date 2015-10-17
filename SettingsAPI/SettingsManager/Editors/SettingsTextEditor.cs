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
    public partial class SettingsTextEditor : UserControl, ISettingValueEditor
    {
        public SettingsTextEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "No string";
            }
        }

        public string Value
        {
            get
            {
                if (textEdit1.EditValue != null)
                    return textEdit1.Text;

                return null;
            }

            set
            {
                textEdit1.Text = value;
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.String;
            }
        }

        public bool ValidateValue()
        {
            return true;
        }

        public bool ValidateValue(string value)
        {
            return true;
        }
    }
}
