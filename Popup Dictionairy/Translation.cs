using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popup_Dictionairy
{
    class Translation
    {
        string fromLanguage;
        string toLanguage;
        int correctAnswers;
        DateTime lastCorrectAnswerDate;

        public string Translation { get; set; }
    }
}
