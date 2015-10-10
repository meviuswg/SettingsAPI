using SettingsAPIData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public abstract class BaseDataController : IDisposable
    {
        private SettingsDbContext context;
        private IApiKey keyProvider;

        public BaseDataController(SettingsDbContext context, IApiKey apiKeyProvider)
        {
            this.context = context;
            this.keyProvider = apiKeyProvider;
        }

        protected SettingsDbContext Context
        {
            get { return context; }
        }

        #region Current Identity
        protected ApiKeyData CurrentApiKey
        {
            get
            {
                return context.ApiKeys.SingleOrDefault(a => a.ApiKey == keyProvider.Key);
            }
        }

        protected int CurrentIdentity
        {
            get
            {
                if (CurrentApiKey != null)
                    return CurrentApiKey.Id;

                return -1;
            }
        } 

        protected bool IsMasterKey
        {
            get { return keyProvider.Key == Constants.MASTER_API_KEY; }
        }

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
        #endregion
        #endregion


    }
}
