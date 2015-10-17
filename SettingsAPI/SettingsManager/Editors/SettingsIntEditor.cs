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
    public partial class SettingsIntEditor : UserControl, ISettingValueEditor
    {
        public SettingsIntEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "Not a valid Int";
            }
        }

        public string Value
        {
            get
            {
                if (textEdit1.EditValue != null)
                    return textEdit1.EditValue.ToString();
                return null;
            }

            set
            {
               if(ValidateValue(value))
                {
                    textEdit1.EditValue = int.Parse(value);
                }
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.Int;
            }
        }

        public bool ValidateValue()
        {
            bool validate = ValidateValue(textEdit1.Text);

            if (!validate)
            {
                textEdit1.ErrorText = ValidationMessage;
            }

            return validate;
        }

        public bool ValidateValue(string value)
        {
            int d;
            return int.TryParse(value, out d);
        }
    }
}
