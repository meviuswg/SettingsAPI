using SettingsAPIData.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAPIData
{
    public class SettingsRepository : IDisposable, ISettingsRepository
    {
        private SettingsDbContext context;
        private IApiKey apiKey;

        public SettingsRepository(SettingsDbContext context, IApiKey apiKeyProvider)
        {
            this.context = context;
            this.apiKey = apiKeyProvider;

        }
         
        public  SettingsDbContext Context
        {
            get { return context; }
        }

        #region Current Identity
        private ApiKeyData _currentApiKey;
        public ApiKeyData CurrentApiKey
        {
            get
            {
                if (apiKey == null || string.IsNullOrWhiteSpace(apiKey.Key))
                {
                    throw new ArgumentNullException("ApiKey");
                }

                if(_currentApiKey == null)

                {
                    _currentApiKey = context.ApiKeys.SingleOrDefault(a => a.ApiKey == apiKey.Key);
                }

                return _currentApiKey;
            }
        }

        public int CurrentIdentity
        {
            get
            {
                if (CurrentApiKey != null)
                    return CurrentApiKey.Id;

                return -1;
            }
        }

        private string _mk;
        private bool _mkSet;

        public bool IsMasterKey
        {
            get
            {
                if (_mkSet == false)
                {
                    var masterkey = context.ApiKeys.Find(Constants.SYSTEM_MASTER_KEY_ID);

                    if (masterkey == null)
                    {
                        throw new ApplicationException(Constants.ERROR_NO_MASTER_KEY);
                    }

                    _mk = masterkey.ApiKey;

                    _mkSet = true;
                }

                return CurrentApiKey != null && string.Equals(CurrentApiKey.ApiKey, _mk);
            }

        }
        #endregion
         
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
   


    }
}
