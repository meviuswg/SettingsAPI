﻿using SettingsAPIRepository.Data;
using SettingsAPIRepository.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingsAPIRepository
{
    public class ValidationRepository : IValidationRepository
    {
        private SettingsDbContext context;

        public ValidationRepository()
        {
        }

        private SettingsDbContext Context
        {
            get
            {

                try
                {
                    context = new SettingsDbContext();
                    var test = context.Applications.Count();
                   
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    throw new SettingsStoreException(Constants.ERROR_STORE_UNAVAILABLE, ex);
                }

                try
                {
                    return context;
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    throw new SettingsStoreException(Constants.ERROR_STORE_EXCEPTION, ex);
                }
            }
        }
 
        

        public ApiKeyData GetKey(string key)
        {
            ApiKeyData data = Context.ApiKeys.SingleOrDefault(a => a.ApiKey == key);
            return data;
        }

        public void SetUsed(string apiKey)
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
    }
}