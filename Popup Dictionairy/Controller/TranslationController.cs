using PopupDictionairy.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopupDictionairy.App.Controller
{
    public class TranslationsController
    {
        private List<Translation> _internalCollection;
        private ITranslationsPersistenceProvider persistenceProvider;
        private bool initialized;
        private ITranslationQuestionSelector questionSelector;


        public TranslationsController(ITranslationsPersistenceProvider persistenceProvider)
            : this(persistenceProvider, new TranslationQuestionSelector())
        {

        }
        public TranslationsController(ITranslationsPersistenceProvider persistenceProvider, ITranslationQuestionSelector questionSelector)
        {
            this.persistenceProvider = persistenceProvider;
            this.questionSelector = questionSelector;
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

        public IEnumerable<Translation> GetTranslationsForSession(int take)
        {
            return questionSelector.GetBatch(take, Translations);
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