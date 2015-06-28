using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popup_Dictionairy
{
    internal class QuestionSession
    {
        private Translation[] questionList;
        private int size;
        private int currentTranslationIndex = 0;

        public QuestionSession()
            : this(10)
        {
        }

        public QuestionSession(int numberOfQuestions)
        {
            size = numberOfQuestions;
        }

        public Translation Next()
        {
            currentTranslationIndex++;

            if (currentTranslationIndex > questionList.Length - 1)
                return null;

            var translation = questionList[currentTranslationIndex];
            return translation;
        }

        private void Init()
        {
            questionList = (from translation in TranslationProvider.Instance.Translations
                            select translation).Take(size).ToArray();
        }
    }
}