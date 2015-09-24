using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PopupDictionairy.App.Model
{
    public class QuestionSession
    {
        //private Translation[] questionList;
       Translation[] translations;
        private int size;
        int currentTranslationIndex;
 
 
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