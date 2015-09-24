using PopupDictionairy.App.Model;
using PopupDictionairy.Util;
using System.Collections.Generic;
using System.IO;

namespace PopupDictionairy.App.Controller
{
    public class FileSystemPersistenceProvider : ITranslationsPersistenceProvider
    {
        private string _userAppdataPath;
        private string _fileName;

        private string _filePath;

        public FileSystemPersistenceProvider(string userAppdataPath)
            : this(userAppdataPath, "translations.jsn")
        {
            
        }

        public FileSystemPersistenceProvider(string userAppdataPath, string fileName)
        {
            _userAppdataPath = userAppdataPath;
            _fileName = fileName;

            _filePath = Path.Combine(_userAppdataPath, _fileName);
        }

        public void SaveTranslations(IEnumerable<Translation> translations)
        {
            Directory.CreateDirectory(_userAppdataPath);
            PersistenceHelper.Save(translations, _filePath);
        }

        public IEnumerable<Translation> LoadTranslations()
        {
            List<Translation> translations = new List<Translation>();

            string fileName = Path.Combine(_userAppdataPath, _filePath);
            var data = PersistenceHelper.Load<List<Translation>>(fileName);
            if (data != null)
            {
                translations = data;
            }

            return translations;
        }
    }
}