using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Popup_Dictionairy
{
    public partial class TranslationsForm : Form
    {
        private BindingList<Translation> translationList;

        public TranslationsForm()
        {
            InitializeComponent();

            translationList = new BindingList<Translation>(TranslationProvider.Instance.Translations.ToList());
            this.dataGridView1.DataSource = translationList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TranslationProvider.Instance.Translations.Clear();
            TranslationProvider.Instance.Translations.AddRange(translationList);
            TranslationProvider.Instance.Save();
            this.Close();
        }
    }
}