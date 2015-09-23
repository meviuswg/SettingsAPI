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
                if(t.ToLanguage == givenAnswer)
                {
                    t.CorrectAnswers += 1;
                    t.LastCorrectAnswer = DateTime.Now;
                    MessageBox.Show("You answered correctly!");

                    session.UpdateCurrent(t);
                }
                
            }

            //Get next translation
            t = session.Next();
            txtAnswer.Text = String.Empty;
            if (t != null) 
            {
                lblQuestion.Text = t.FromLanguage;
                return;
            }
            session.SaveScore();
            this.Close();


        }

        
    }
}
