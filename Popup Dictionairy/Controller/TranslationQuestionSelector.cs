using PopupDictionairy.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopupDictionairy.App.Controller
{
    internal class TranslationQuestionSelector : ITranslationQuestionSelector
    {
        public IEnumerable<Translation> GetBatch(int take, IEnumerable<Translation> source)
        {
            List<Translation> choosenTranslations = new List<Translation>();

            var allTranslations = (from translation in source
                                   where translation.CorrectAnswers < 3 //Ik werk dus met deze set, en deze set sla ik later op. De volgende keer dat ik een questionsession laad, worden degene die
                                   //ik de vorige sessie niet goed heb beantwoord dus niet mee opgehaald. Als ik na die sessie opsla mis ik dus translations.
                                   select translation).ToList();

            if (take > allTranslations.Count)
            {
                take = allTranslations.Count;
            }

            //questionList = new Translation[size];
            Random rnd = new Random();

            while (choosenTranslations.Count < take)
            {
                int translationIndex = rnd.Next(0, allTranslations.Count);
                if (!choosenTranslations.Contains(allTranslations[translationIndex]))
                    choosenTranslations.Add(allTranslations[translationIndex]);
            }

            return choosenTranslations;
        }
    }
}