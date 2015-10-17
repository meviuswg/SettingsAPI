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
using System.Xml;

namespace SettingsManager.Editors
{
    public partial class SettingsXmlEditor : UserControl, ISettingValueEditor
    {
        public SettingsXmlEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "Not valid Xml";
            }
        }

        public string Value
        {
            get
            {
                return memoEdit1.ToString();
            }

            set
            {
                if(ValidateValue(value))
                {
                    memoEdit1.Text = value;
                } 
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.Xml;
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
                XmlDocument d = new XmlDocument();
                d.LoadXml(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
