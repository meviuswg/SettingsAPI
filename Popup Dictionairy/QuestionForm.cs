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
    public partial class QuestionForm : Form
    {
        QuestionSession session;
        Translation t;

        public QuestionForm()
        {
            InitializeComponent();

            session = new QuestionSession(2);
            this.GetAndDisplayTranslation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.GetAndDisplayTranslation();
        }

        private void GetAndDisplayTranslation()
        {
            t = session.Next();
            if (t != null) 
            {
                lblQuestion.Text = t.FromLanguage;
                return;
            }
            this.Close();


        }
    }
}
