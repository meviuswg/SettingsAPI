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
using SettingsAPIClient.Util;

namespace SettingsManager.Editors
{
    public partial class SettingsImageEditor : UserControl, ISettingValueEditor
    {
        public SettingsImageEditor()
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
                if (pictureEdit1.Image != null)
                {
                   return SerializationHelper.ImageToString(pictureEdit1.Image);
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    pictureEdit1.Image = SerializationHelper.ToImage(value);
                }
            }
        }

        public ValueDataType ValueType
        {
            get
            {
                return ValueDataType.Image;
            }
        }

        public bool ValidateValue()
        {
            return true;
        }

        public bool ValidateValue(string value)
        {
            try
            {
                SerializationHelper.ToImage(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
