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
        Translation[] allTranslations;
        private int size;
        private int currentTranslationIndex = -1;

        public QuestionSession()
            : this(10)
        {
        }

        public QuestionSession(int numberOfQuestions)
        {
            size = numberOfQuestions;
            this.Init();
        }

        public Translation Next()
        {
            currentTranslationIndex++;

            if (currentTranslationIndex > questionList.Length - 1)
                return null;

            var translation = questionList[currentTranslationIndex];
            return translation;
        }

        public void UpdateCurrent(Translation t)
        {
            questionList[currentTranslationIndex] = t;

        }

        private void Init()
        {
            int questionIndex = 0;
            allTranslations = (   from translation in TranslationProvider.Instance.Translations
                                                select translation).ToArray();



            if(size > allTranslations.Length)
            {
                size = allTranslations.Length;
            }
            questionList = new Translation[size];
            Random rnd = new Random();

            while (questionIndex < size)
            {
                int translationIndex = rnd.Next(0, allTranslations.Length);
                Translation t = allTranslations[translationIndex];
                if(Array.IndexOf(questionList, t) == -1)
                {
                    questionList[questionIndex] = t;
                    questionIndex++;
                }

            }
        }

        public void SaveScore()
        {

            //Nope dit is hem ook niet. 
            foreach (Translation t in questionList)
            { 
                
            
            }

        }

    }
}