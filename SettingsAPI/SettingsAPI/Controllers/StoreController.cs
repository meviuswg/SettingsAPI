using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SettingsAPI.Controllers
{
    public class directoryController : BaseApiController
    {
        public directoryController()
        {

        }

        //api/directory
        public IEnumerable<DirectoryModel> Get(string applicationName)
        {
            return null;
            //return (from a in db.Directories
            //        where a.Application.Name == applicationName
            //        select new DirectoryModel
            //        {
            //            Name = a.Name,
            //            Description = a.description,
            //            Application = a.Application.Name
            //        });
        }

        public IEnumerable<DirectoryModel> Get(string applicationName, string name)
        {
            return null;
        }

        // POST api/application
        public HttpResponseMessage Post(string applicationName, [FromBody] DirectoryModel value)
        {
            return null;
        }


        // DELETE api/application/name
        public void Delete(int id)
        {
        }
    }
}
