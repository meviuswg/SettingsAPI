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
    public partial class SettingsDateTimeEditor : UserControl, ISettingValueEditor
    {
        public SettingsDateTimeEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Value
        {
            get
            {
                if (dateEdit1.EditValue != null)
                {
                    return dateEdit1.EditValue.ToString();
                }
                return null;
            }

            set
            {
                if (value != null)
                {
                    dateEdit1.EditValue = DateTime.Parse(value);
                }
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.DateTime;
            }
        }

        public bool ValidateValue()
        {
            return true;
        }

        public bool ValidateValue(string value)
        {
            DateTime d;
            return DateTime.TryParse(value, out d);
        }
    }
}
