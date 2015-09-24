using PopupDictionairy.App.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopupDictionairy.App
{
    public partial class QuestionForm : Form
    {
        QuestionSession session;
        Translation current;         

        public QuestionForm(IEnumerable<Translation> translations)
        {
            InitializeComponent();

            session = new QuestionSession(translations);            
            this.ProcessAndDisplayTranslation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ProcessAndDisplayTranslation();
        }

        private void ProcessAndDisplayTranslation()
        {
            //Process given translation
            string givenAnswer = txtAnswer.Text;
            
            if (!String.IsNullOrEmpty(givenAnswer))
            {
                if (current.ToLanguage == givenAnswer)
                {
                    current.CorrectAnswers += 1;
                    current.LastCorrectAnswer = DateTime.Now;
                    MessageBox.Show("You answered correctly!");
                }
                else
                {
                    MessageBox.Show("You're Stupid!!!","You answer was not correctly",  MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                
            }

            //Get next translation
            current = session.Next();
            txtAnswer.Text = String.Empty;
            if (current != null) 
            {
                lblQuestion.Text = current.FromLanguage;
                return;
            } 
 
            this.Close();


        }

        
    }
}
