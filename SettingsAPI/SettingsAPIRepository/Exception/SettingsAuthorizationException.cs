using SettingsAPIShared;

namespace SettingsAPIRepository
{
    public enum AuthorizationLevel
    {
        Read, Write, Create, Delete
    }

    public enum AuthorizationScope
    {
        Application, Directory, Setting,
        Version,
        ApiKey
    }

    public class SettingsAuthorizationException : SettingsStoreException
    {
        private int _identity;
        private AuthorizationLevel _level;
        private string _objectName;
        private AuthorizationScope _scope;

        public SettingsAuthorizationException(AuthorizationScope scope, AuthorizationLevel level, string objectName, int identity)
            : base(string.Format("{0} {1} {2}: {3}.", level, scope, objectName ?? "unknown", Constants.ERROR_ACCESS_DENIED))
        {
            _scope = scope;
            _level = level;
            _objectName = objectName;
            _identity = identity;
        }

        public int Identity { get { return _identity; } }
        public AuthorizationLevel Level { get { return _level; } }
        public string ObjectName { get { return _objectName; } }
        public AuthorizationScope Scope { get { return _scope; } }
    }
}