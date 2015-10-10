using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SettingsAPI.Controllers
{
    public class VersionController : BaseApiController
    { 
        public VersionController()
        {

        }

        public IEnumerable<RepositoryModel> Get(string applicationName)
        {
            return null;
        }


        public IEnumerable<RepositoryModel> Get(string applicationName, int version)
        {
            return null;

        }



        // POST api/application
        public HttpResponseMessage Post(string applicationName, int version)
        {
            return null;
        }
    }
}
