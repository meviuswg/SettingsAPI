using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popup_Dictionairy
{
    internal class QuestionsController
    {
        private List<Translation> questionList;

        public QuestionsController()
        {
            questionList = TranslationProvider.Instance.Translations;
        }

        //Return list of new questions
        public List<Translation> Questions()
        {
            //Get top n of questions

            return questionList;
        }

        //Beoordeel answers
    }
}