using SettingsAPIClient;
using SettingsManager.Editors;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsManager
{
    public partial class SettingEditForm : Form
    {
        private Setting setting;
        private WorkingDirectoryObject directory;

        public SettingEditForm(Setting setting, WorkingDirectoryObject directory)
        {
            InitializeComponent();

            this.setting = setting;
            this.directory = directory;

            comboBox1.DataSource = Enum.GetNames(typeof(ValueDataType)).OrderBy(s => s).ToArray();
            comboBox1.SelectedItem = "String";

            if (setting != null)
            {
                SetEditorWithValue(setting);

                textKey.Text = setting.Key;
                textInfo.Text = setting.Info;
                textObjectId.EditValue = setting.ObjectId;
            }
            else
            {
                SetEditor(ValueDataType.String); 
            }

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            var selected = (ValueDataType)Enum.Parse(typeof(ValueDataType), comboBox1.SelectedItem.ToString());

            if (Editor !=null && selected != Editor.ValueType)
                SetEditor(selected);
        }

        public ISettingValueEditor Editor { get; set; }

        public object ISettingsEditor { get; private set; }

        public Setting Setting { get { return setting; } }



        private void SetEditor(ValueDataType dataType)
        {
            switch (dataType)
            { 
                case ValueDataType.Int:
                    Editor = new SettingsIntEditor();
                    break;

                case ValueDataType.String:
                    Editor = new SettingsStringEditor();
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
                    Editor = new SettingsCustomEditor();
                    break;
            }

            panelEditor.Controls.Clear();
            comboBox1.SelectedItem = Editor.ValueType.ToString();
            ((Control)Editor).Dock = DockStyle.Fill;
            panelEditor.Controls.Add((Control)Editor);
        }

        private void SetEditorWithValue(Setting setting)
        {
            SetEditor(setting.ValueType);

            try
            {
                if (Editor.ValidateValue(setting.Value))
                {
                    Editor.Value = setting.Value;
                }
                else
                {
                    throw new ArgumentException("Value");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not initialize the editor for type '{0}' with the provided value.", setting.ValueType.ToString());
                SetEditor(ValueDataType.Custom);
                Editor.Value = setting.Value;
            }
        }

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

                if (setting == null && await directory.Exists(int.Parse(textObjectId.Text), textKey.Text))
                {
                    textKey.ErrorText = "A Setting Key with this name already in exist";
                    return false;
                }

                if (setting == null)
                {
                    setting = new Setting();
                }

                if (!Editor.ValidateValue())
                {
                    return false;
                }

                setting.Value = Editor.Value;
                setting.ValueType = Editor.ValueType;
                setting.Info = textInfo.Text;
                setting.ObjectId = int.Parse(textObjectId.Text);
                setting.Key = textKey.Text.Trim();

                return true;
            }
        } 
    }
}