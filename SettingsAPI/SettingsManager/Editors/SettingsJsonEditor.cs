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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SettingsManager.Editors
{
    public partial class SettingsJsonEditor : UserControl, ISettingValueEditor
    {
        public SettingsJsonEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "Not valid Json";
            }
        }

        public string Value
        {
            get
            {
                return memoEdit1.Text;
            }

            set
            {
                memoEdit1.Text = value;
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.Json;
            }
        }

        public bool ValidateValue()
        {
            bool validate = ValidateValue(memoEdit1.Text);

            if (!validate)
            {
                memoEdit1.ErrorText = ValidationMessage;
            }

            return validate;

        }

        public bool ValidateValue(string value)
        {
            try
            {
                JToken.Parse(memoEdit1.Text);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
