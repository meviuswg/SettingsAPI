using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingStore
    {
        private string _applicationName;
        private string _directory;
        private int _version;
        private int? _objectId;


        public SettingStore(string application, string directory)
        {
            _applicationName = application;
            _directory = directory;
            _version = 1;
            _objectId = null;
        }
        public SettingStore(string application, int version, string directory)
        {
            _applicationName = application;
            _directory = directory;
            _version = version;
            _objectId = null;
        }

        public SettingStore(string application, int version, string directory, int objectId)
        {
            _applicationName = application;
            _directory = directory;
            _version = version;
            _objectId = objectId;
        }
        public string ApplicationName { get { return _applicationName; } }
        public string Directory { get { return _directory; } }
        public int Version { get { return _version; } }
        public int? ObjectId { get { return _objectId; } }

    }
}
