using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Popup_Dictionairy
{
    internal class QuestionSession
    {
        //private Translation[] questionList;
        Translation[] allTranslations;
        private int size;
        private int?[] chosenQuestions;
        int currentTranslationIndex;
        int questionNumber;

        public QuestionSession()
            : this(10)
        {
        }

        public QuestionSession(int numberOfQuestions)
        {
            size = numberOfQuestions;
            chosenQuestions = new int?[size];
            this.Init();
        }

        public Translation Next()
        {



            if (questionNumber > chosenQuestions.Length - 1)
                return null;
            currentTranslationIndex = chosenQuestions[questionNumber].Value;
            var translation = allTranslations[currentTranslationIndex];
            questionNumber++;
            return translation;
        }

        public void UpdateCurrent(Translation t)
        {
            allTranslations[currentTranslationIndex] = t;

        }

        private void Init()
        {
            questionNumber = 0;

            allTranslations = (from translation in TranslationProvider.Instance.Translations
                               where translation.CorrectAnswers < 3 //Ik werk dus met deze set, en deze set sla ik later op. De volgende keer dat ik een questionsession laad, worden degene die 
                               //ik de vorige sessie niet goed heb beantwoord dus niet mee opgehaald. Als ik na die sessie opsla mis ik dus translations.
                               select translation).ToArray();


            if (size > allTranslations.Length)
            {
                size = allTranslations.Length;
            }

            //questionList = new Translation[size];
            Random rnd = new Random();
            int questionIndex = 0;
            while (questionIndex < size)
            {
                int translationIndex = rnd.Next(0, allTranslations.Length);

                if (Array.IndexOf(chosenQuestions, translationIndex) == -1)
                {
                    chosenQuestions[questionIndex] = translationIndex;
                    questionIndex++;
                }
            }
        }

        public void SaveScore()
        {

            //Nope dit is hem ook niet. 
            TranslationProvider.Instance.Translations.Clear();
            TranslationProvider.Instance.Translations.AddRange(allTranslations);
            TranslationProvider.Instance.Save();

        }

    }
}