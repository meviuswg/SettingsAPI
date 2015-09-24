using PopupDictionairy.App.Model;
using System.Collections.Generic;

namespace PopupDictionairy.App.Controller
{
    public interface ITranslationsPersistenceProvider
    {
        IEnumerable<Translation> LoadTranslations();

        void SaveTranslations(IEnumerable<Translation> translations);
    }
}