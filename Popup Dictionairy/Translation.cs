using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popup_Dictionairy
{
   internal class Translation
    {
        private string fromLanguage;
        private string toLanguage;
        private int correctAnswers;
        private DateTime lastCorrectAnswer;

        
        public Translation()
        {
            
        }
        
        public Translation(string fromLanguage, string toLanguage)
        {
            this.fromLanguage = fromLanguage;
            this.toLanguage = toLanguage;
        }
        
        public string FromLanguage
        {
            get { return fromLanguage; }
            set { fromLanguage = value; }
        }

        public string ToLanguage
        {
            get { return toLanguage; }
            set { toLanguage = value;}
        }

        public int CorrectAnswers
        {
            get { return correctAnswers; }
            set { correctAnswers = value; }
        }

        public DateTime LastCorrectAnswer
        {
            get { return lastCorrectAnswer; }
            set { lastCorrectAnswer = value; }
        }
 
    }
}
