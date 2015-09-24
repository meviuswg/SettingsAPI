using PopupDictionairy.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopupDictionary.App.Controller
{
    public class TranslationsController
    {
        private List<Translation> _internalCollection;
        private ITranslationsPersistenceProvider persistenceProvider;
        private bool initialized;

        public TranslationsController(ITranslationsPersistenceProvider persistenceProvider)
        {
            this.persistenceProvider = persistenceProvider;
        }

        public List<Translation> Translations
        {
            get
            {
                if (!initialized)
                {
                    Load();
                    initialized = true;
                }
                return _internalCollection;
            }
        }

        public List<Translation> GetTranslationsForSession(int take)
        {
            List<Translation> choosenTranslations = new List<Translation>();

            var allTranslations = (from translation in Translations
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

        public void Load()
        {
            _internalCollection = persistenceProvider.LoadTranslations().ToList();
        }

        public void Save()
        {
            persistenceProvider.SaveTranslations(Translations);
        }
    }
}