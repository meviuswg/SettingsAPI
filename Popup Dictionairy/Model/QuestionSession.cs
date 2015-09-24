using System.Collections.Generic;
using System.Linq;

namespace PopupDictionairy.App.Model
{
    public class QuestionSession : IQuestionSession
    {
        //private Translation[] questionList;
        private Translation[] translations;

        private int size;
        private int currentTranslationIndex;

        public QuestionSession(IEnumerable<Translation> translations)
        {
            this.translations = translations.ToArray();
            size = translations.Count();
            currentTranslationIndex = 0;
        }

        public Translation Next()
        {
            if (currentTranslationIndex > translations.Count() - 1)
                return null;

            var translation = translations[currentTranslationIndex];
            currentTranslationIndex++;
            return translation;
        }
    }
}