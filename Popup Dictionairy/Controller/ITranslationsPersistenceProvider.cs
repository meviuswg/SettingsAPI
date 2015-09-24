using PopupDictionairy.App.Model;
using System;
using System.Collections.Generic;
namespace PopupDictionary.App.Controller
{
    public interface ITranslationsPersistenceProvider
    {
        IEnumerable<Translation> LoadTranslations();
        void SaveTranslations(IEnumerable<Translation> translations);
    }
}
