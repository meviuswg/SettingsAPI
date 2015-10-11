using SettingsAPIData.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace SettingsAPIData
{
    public interface ISettingsRepository
    {
        SettingsDbContext Context { get; }
        ApiKeyData CurrentApiKey { get; }
        int CurrentIdentity { get; }
        bool IsMasterKey { get; }

        void Dispose();
 
    }
}