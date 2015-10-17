using DevExpress.XtraEditors;
using SettingsAPIClient;
using SettingsManager.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class SettingEditForm : Form
    {
        SettingsAPIClient.SettingsManager settingsManager;

        private Setting setting;
        public SettingEditForm(Setting setting, SettingsAPIClient.SettingsManager settingsManager)
        {
            InitializeComponent();

            this.setting = setting;
            this.settingsManager = settingsManager;

            if (setting != null)
            {
                comboBoxEdit1.SelectedItem = setting.ValueType.ToString();

                SetEditor(setting.ValueType);
                textKey.Text = setting.Key;
                textObjectId.EditValue = setting.ObjectId;
                Editor.Value = setting.Value;
            } 
            else
            {
                SetEditor(ValueDataType.String);
                comboBoxEdit1.EditValue = ValueDataType.String.ToString();
            }

            comboBoxEdit1.Properties.EditValueChanged += comboBoxEdit1_Properties_EditValueChanged; 

        }
        private async Task<bool> ValidateInputAsync()
        {

            if (string.IsNullOrWhiteSpace(textKey.Text))
            {
                textKey.ErrorText = "Enter a Name";
                return false;
            }
            else
            {
                if (textObjectId.EditValue == null)
                {
                    textObjectId.ErrorText = "Invalid ObjectId";
                    return false;
                }

                if (setting == null && await settingsManager.ExistsAsync(textKey.Text))
                {
                    textKey.ErrorText = "A Setting Key with this name already in exist";
                    return false;
                }

                if (setting == null)
                {
                    setting = new Setting();
                }

                if(!Editor.ValidateValue())
                {
                    return false;
                }

                setting.Value = Editor.Value;
                setting.ValueType = Editor.ValueType; 
                setting.ObjectId = int.Parse(textObjectId.Text);
                setting.Key = textKey.Text.Trim();


                return true;
            }
        }

        public ISettingValueEditor Editor { get; set; }

        public Setting Setting { get { return setting; } }

        public object ISettingsEditor { get; private set; }

        private async void simpleButtonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (await ValidateInputAsync())
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (SettingsException ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void simpleCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetEditor(ValueDataType dataType)
        {


            switch (dataType)
            {
                case ValueDataType.Int:

                case ValueDataType.String:
                    Editor = new SettingsTextEditor();
                    break;
                case ValueDataType.Decimal:
                    Editor = new SettingsDecimalEditor();
                    break;
                case ValueDataType.ByteArray:
                    Editor = new SettingsBinaryEditor();
                    break;
                case ValueDataType.DateTime:
                    Editor = new SettingsDateTimeEditor();
                    break;
                case ValueDataType.Bool:
                    Editor = new SettingsBooleanEditor();
                    break;
                case ValueDataType.Json:
                    Editor = new SettingsJsonEditor();
                    break;
                case ValueDataType.Xml:
                    Editor = new SettingsXmlEditor();
                    break;
                case ValueDataType.Image:
                    Editor = new SettingsImageEditor();
                    break;
                default:
                    break;
            }

            panelEditor.Controls.Clear();

            ((Control)Editor).Dock = DockStyle.Fill;
            panelEditor.Controls.Add((Control)Editor);
        }

        private void comboBoxEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            ValueDataType data = (ValueDataType)Enum.Parse(typeof(ValueDataType), comboBoxEdit1.EditValue.ToString());
            SetEditor(data);
        }
    }
}
