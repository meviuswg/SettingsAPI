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

        public string FromLanguage
        {
            get { return fromLanguage; }
            set { fromLanguage = value; }
        }

        private string toLanguage;

        public string ToLanguage
        {
            get { return toLanguage; }
            set { toLanguage = value; }
        }

        //private int correctAnswers;
        //private DateTime lastCorrectAnswerDate;
    }
}