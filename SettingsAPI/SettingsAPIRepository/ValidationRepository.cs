using SettingsAPIRepository.Data;
using SettingsAPIShared;
using System;
using System.Linq;

namespace SettingsAPIRepository
{
    public class ValidationRepository : IValidationRepository, IDisposable
    {
        private SettingsDbContext _context;

        public ValidationRepository()
        {
            _context = new SettingsDbContext();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public ApiKeyData GetApiKey(string apiKey)
        {
            return _context.ApiKeys.SingleOrDefault(a => a.ApiKey == apiKey);
        }

        public bool IsValid(string key, out int keyId)
        {
            keyId = 0;

            try
            {
                using (var context = new SettingsDbContext())
                {
                    ApiKeyData data = context.ApiKeys.SingleOrDefault(a => a.ApiKey == key);
                    if (data != null)
                    {
                        keyId = data.Id;
                        return data.Active;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw new SettingsStoreException(Constants.ERROR_STORE_UNAVAILABLE, ex);
            }
        }

        public void SetUsed(string apiKey)
        {
            try
            {
                using (var context = new SettingsDbContext())
                {
                    var data = context.ApiKeys.SingleOrDefault(a => a.ApiKey == apiKey);

                    if (data != null)
                    {
                        data.LastUsed = DateTime.UtcNow;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                throw new SettingsStoreException(Constants.ERROR_STORE_UNAVAILABLE, ex);
            }
        }
    }
}