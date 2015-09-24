using Newtonsoft.Json;
using System;
using System.IO;

namespace PopupDictionairy.Util
{
    internal class PersistenceHelper
    {
        public PersistenceHelper()
        {
        }

        public static T Load<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            string content = ReadFromDisk(path);
            return Deserialize<T>(content);
        }

        public static void Save(object data, string path)
        {
            string contents = Serialize(data);
            WriteToDisk(contents, path);
        }

        private static string ReadFromDisk(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return string.Empty;
        }

        private static void WriteToDisk(string contents, string path)
        {
            File.WriteAllText(path, contents);
        }

        private static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        private static T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}