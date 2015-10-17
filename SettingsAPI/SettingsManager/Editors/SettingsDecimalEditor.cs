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
    public partial class SettingsDecimalEditor : UserControl, ISettingValueEditor
    {
        public SettingsDecimalEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "Not a valid Decimal";
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
                    textEdit1.EditValue = decimal.Parse(value);
                }
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.Decimal;
            }
        }

        public bool ValidateValue()
        {
            return ValidateValue(textEdit1.Text);
        }

        public bool ValidateValue(string value)
        {
            Decimal d;
            return Decimal.TryParse(value, out d);
        }
    }
}
