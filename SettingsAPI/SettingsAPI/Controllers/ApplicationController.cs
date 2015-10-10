using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SettingsAPI.Controllers
{
    public class ApplicationController : BaseApiController
    {
        public ApplicationController()
        {

        }

        //api/application
        public IEnumerable<ApplicationModel> Get()
        {
            return null;
        }


        public IEnumerable<ApplicationModel> Get(string name)
        {
            return null;
        }

        // POST api/application
        public HttpResponseMessage Post([FromBody]ApplicationModel value)
        {
            return null;
        } 

        // DELETE api/application/name
        public void Delete(int id)
        {
        }
    }
}
