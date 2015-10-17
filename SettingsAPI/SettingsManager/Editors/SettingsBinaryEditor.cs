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
using SettingsAPIClient.Util;

namespace SettingsManager.Editors
{
    public partial class SettingsBinaryEditor : UserControl, ISettingValueEditor
    {
        public SettingsBinaryEditor()
        {
            InitializeComponent();
        }

        public string ValidationMessage
        {
            get
            {
                return "Not valid Binary data";
            }
        }

        byte[] byteArray = null;

        public string Value
        {
            get
            {
                if (byteArray != null)
                {
                   return SerializationHelper.ToBase64String(byteArray);
                }

                return null;
            }

            set
            {
                byteArray = SerializationHelper.FromBase64String(value);
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
            return true;         
        }

        public bool ValidateValue(string value)
        {
            return true;
        }
    }
}
