using SettingsAPIData;
using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace SettingsAPI.Controllers
{
    [Authorize]
    public class SettingsController : BaseApiController
    {
        private ISettingsRepository controller;

        public SettingsController(ISettingsRepository controller)
        {
            this.controller = controller;
        }

        [HttpGet]
        [Route("api/settings/{applicationName}/{version}/{directory}/")]
        [ResponseType(typeof(DirectoryModel))]
        public IHttpActionResult Get(string applicationName, int version, string directory)
        {
            return Get(new SettingStore(applicationName, version, directory));
        }

        [HttpGet]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, int version, string directory, int objectId)
        {
            return Get(new SettingStore(applicationName, version, directory, objectId));
        }

        [HttpGet]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}/{key}")]
        [ResponseType(typeof(SettingModel))]
        public IHttpActionResult Get(string applicationName, int version, string directory, int objectId, string key)
        {
            return Get(new SettingStore(applicationName, version, directory, objectId), key);
        }

        public IHttpActionResult Get(SettingStore store)
        {
            try
            {
                return Ok(controller.GetSettings(store));

            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        public IHttpActionResult Get(SettingStore store, string key)
        {
            try
            {
                return Ok(controller.GetSetting(store, key));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}/{key}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(string applicationName, int version, string directory, int objectId, string key, [FromBody]string value)
        { 
     
            return Post(applicationName, version, directory, objectId, new SettingModel { Key = key, Value = value } );
        }

        [HttpPost]
        [Route("api/settings/{applicationName}/{version}/{directory}/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PostCollection(string applicationName, int version, string directory, [FromBody]IEnumerable<SettingModel> value)
        {
            var store = new SettingStore(applicationName, version, directory);

            try
            {
                controller.SaveSettings(store, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("api/settings/{applicationName}/{version}/{directory}/{objectId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(string applicationName, int version, string directory, int objectId, [FromBody]SettingModel value)
        {
            var store = new SettingStore(applicationName, version, directory, objectId);

            try
            {
                controller.SaveSetting(store, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}