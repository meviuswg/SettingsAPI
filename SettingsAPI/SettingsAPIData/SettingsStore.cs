using SettingsAPIData.Data;
using SettingsAPIShared;
using System;
using System.Linq;

namespace SettingsAPIData
{
    public class SettingsStore : IDisposable, ISettingsStore
    {
        private SettingsDbContext context;

        public SettingsStore(SettingsDbContext context)
        {
            this.context = context;

#if DEBUG
            context.Database.Log = Log.Logger;
#endif
        }

        public SettingsDbContext Context
        {
            get
            {
                if (!_dbOnline)
                {
                    try
                    {
                        var test = context.Applications.Count();
                        _dbOnline = true;
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(ex);
                        throw new SettingsStoreException(Constants.ERROR_STORE_UNAVAILABLE);
                    }
                }
                try
                {
                    return context;
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    throw new SettingsStoreException(Constants.ERROR_STORE_EXCEPTION);
                }
            }
        }

        private bool _dbOnline = false;

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (context != null)
                        context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public VersionData GetVersion(string applicationName, int version)
        {
            return Context.Versions.SingleOrDefault(v => v.Application.Name == applicationName && v.Version == version);
        }

        public DirectoryData GetDirectory(string applicationName, string directoryName)
        {
            return Context.Directories.SingleOrDefault(d => d.Application.Name == applicationName && d.Name == directoryName);
        }

        #endregion IDisposable Support
    }
}