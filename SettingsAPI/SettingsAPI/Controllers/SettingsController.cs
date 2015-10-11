using SettingsAPIData;
using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SettingsAPI.Controllers
{
    public class SettingsController : BaseApiController
    {
        private ISettingsDataController controller;

        public SettingsController(ISettingsDataController controller)
        {
            this.controller = controller;
        }


        [HttpGet]
        [Route("api/settings/{applicationName}/{version}/{directory}/")]
        public IEnumerable<SettingModel> Get(string applicationName, int version, string directory)
        {
            return Get(new SettingStore(applicationName, version, directory));
        }

        [HttpGet]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}")]
        public IEnumerable<SettingModel> Get(string applicationName, int version, string directory, int objectId)
        {
            return Get(new SettingStore(applicationName, version, directory, objectId));
        }

        [HttpGet]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}/{key}")]
        public SettingModel Get(string applicationName, int version, string directory, int objectId, string key)
        {
            return Get(new SettingStore(applicationName, version, directory, objectId), key);
        }

        public IEnumerable<SettingModel> Get(SettingStore store)
        {
            if (controller.Exists(store))
            {
                if (controller.AllowRead(store))
                {
                    return controller.GetSettings(store);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }               
        }

        public SettingModel Get(SettingStore store, string key)
        {
            if (controller.Exists(store))
            {
                if (controller.AllowRead(store))
                {
                    return controller.GetSetting(store, key);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}/{key}")]
        public HttpResponseMessage Post(string applicationName, int version, string directory, int objectId, string key, [FromBody]string value)
        {
            SettingModel model = new SettingModel { Key = key, ObjectId = objectId, Value = value };
            return Post(applicationName, version, directory, new[] { model });
        }

        [HttpPost]
        [Route("api/settings/{applicationName}/{version}/{directory}/")]
        public HttpResponseMessage Post(string applicationName, int version, string directory, [FromBody]IEnumerable<SettingModel> value)
        {
            var store = new SettingStore(applicationName, version, directory);

            if (controller.Exists(store))
            {
                if (controller.AllowWrite(store))
                {
                    try
                    {
                        controller.SaveSettings(store, value);
                    }
                    catch (SettingsAuthorizationException)
                    {
                        throw new HttpResponseException(HttpStatusCode.Forbidden); 
                    }

                    return OkResponse();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

    }
}