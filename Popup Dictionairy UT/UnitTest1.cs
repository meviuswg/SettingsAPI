using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popup_Dictionairy;
using System.Collections.Generic;

namespace Popup_Dictionairy_UT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddAndReloadTranslationsTest()
        {
            ICollection<Translation> translations;
            int initCount;
            translations = TranslationProvider.Instance.Translations;
            initCount = translations.Count;

            translations.Add(new Translation { FromLanguage = "A", ToLanguage = "B" });

            TranslationProvider.Instance.Save();
            TranslationProvider.Instance.Load();

            translations = TranslationProvider.Instance.Translations;

            Assert.IsTrue(translations.Count - 1 == initCount);

        }
    }
}
