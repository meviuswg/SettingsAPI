﻿using System.Security.Principal;

namespace SettingsAPIRepository
{
    public class ApiIdentity : IIdentity
    {
        private string _key;
        private int _identity;

        public ApiIdentity(string apiKey, int idenity)
        {
            _key = apiKey;
            _identity = idenity;
        }

        public string AuthenticationType
        {
            get
            {
                return "ApiKey";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return _key;
            }
        }

        public int Id
        {
            get
            {
                return _identity;
            }
        }
    }
}