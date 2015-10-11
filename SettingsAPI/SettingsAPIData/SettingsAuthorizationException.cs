using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public enum AuthorizationScope
    {
        Application, Directory, Key
    }

    public enum AuthorizationLevel
    {
        Read, Write, Create, Delete
    }

    public class SettingsAuthorizationException : SettingsStoreException
    {
        private AuthorizationScope _scope;
        private AuthorizationLevel _level;
        private string _objectName;
        private int _identity;

        public SettingsAuthorizationException(AuthorizationScope scope, AuthorizationLevel level, string objectName, int identity) : this(scope, level, objectName, identity, Constants.ERROR_ACCESS_DENIED)
        {

        }
        public SettingsAuthorizationException(AuthorizationScope scope, AuthorizationLevel level, string objectName, int identity, string message) : base(message)
        {
            _scope = scope;
            _level = level;
            _objectName = objectName;
            _identity = identity;
        }

        public AuthorizationScope Scope { get { return _scope; } }
        public AuthorizationLevel Level { get { return _level; } }

        public string ObjectName { get { return _objectName; } }

        public int Identity { get { return _identity; } }

        public string AuthorizationMessage
        {
            get
            {
                return string.Format("{0} {1} {2}: {3}.", Level, Scope, ObjectName, Message);
            }
        }
    }
}
