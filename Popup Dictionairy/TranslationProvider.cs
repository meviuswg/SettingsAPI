using Popup_Dictionairy.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Popup_Dictionairy
{
    public class TranslationProvider
    {
        private List<Translation> translations;
        private static TranslationProvider _instance;

        private TranslationProvider()
        {
            translations = new List<Translation>();
            this.Init();
        }

        public List<Translation> Translations
        {
            get { return translations; }
            //set { translations = value; }
        }

        private void Init()
        {
            Load();
        }

        public static TranslationProvider Instance { get { return _instance ?? (_instance = new TranslationProvider()); } }

        public void Load()
        {
            string fileName = Path.Combine(Application.UserAppDataPath, "translations.jsn");
            var data = PersistenceHelper.Load<List<Translation>>(fileName);
            if (data != null)
            {
                translations = data;
            }
        }
        public void Save()
        {
            PersistenceHelper.Save(translations, Path.Combine(Application.UserAppDataPath, "translations.jsn"));
        }
    }
}