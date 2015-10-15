using SettingsAPIData;
using SettingsAPIData.Model;
using SettingsAPIShared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace SettingsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/settings")]
    public class SettingsController : BaseApiController
    {
        private ISettingsRepository controller;

        public SettingsController(ISettingsRepository controller)
        {
            this.controller = controller;
        }


        [HttpGet]
        [Route("{applicationName}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName)
        {
            return Get(new SettingStore(applicationName, 1, Constants.DEAULT_DIRECTORY_NAME));
        }

        [HttpPost]
        [Route("{applicationName}/{version:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Get(string applicationName, int version, [FromBody] IEnumerable<SettingModel> value)
        {
            return Post(new SettingStore(applicationName, version, Constants.DEAULT_DIRECTORY_NAME), value);
        }

        [HttpGet]
        [Route("{applicationName}/{directory:alpha}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, string directory)
        {
            return Get(new SettingStore(applicationName, 1, directory));
        }


        [HttpPost]
        [Route("{applicationName}/{directory:alpha}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, string directory, [FromBody] IEnumerable<SettingModel> value)
        {
            return Post(new SettingStore(applicationName, 1, directory), value);
        }

        [HttpGet]
        [Route("{applicationName}/{directory:alpha}/{objectId}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, string directory, int objectId)
        {
            return Get(new SettingStore(applicationName, 1, directory, objectId));
        }


        [HttpPost]
        [Route("{applicationName}/{directory:alpha}/{objectId}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, string directory, int objectId, [FromBody] IEnumerable<SettingModel> value)
        {
            return Post(new SettingStore(applicationName, 1, directory, objectId), value);
        }

        [HttpGet]
        [Route("{applicationName}/{version:int=1}/{directory}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, int version, string directory)
        {
            return Get(new SettingStore(applicationName, version, directory));
        }

        [HttpGet]
        [Route("{applicationName}/{version:int=1}/{directory}/{objectId:int=0}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, int version, string directory, int objectId)
        {
            return Get(new SettingStore(applicationName, version, directory, objectId));
        }

        [HttpGet]
        [Route("{applicationName}/{version:int=1}/{directory}/{objectId:int=0}/{key}")]
        [ResponseType(typeof(SettingModel))]
        public IHttpActionResult Get(string applicationName, int version, string directory, int objectId, string key)
        {

            return Get(new SettingStore(applicationName, version, directory, objectId), key);
        }

        [HttpPost]
        [Route("{applicationName}/{version:int=1}/{directory}/{objectId:int=0}/{key}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(string applicationName, int version, string directory, int objectId, string key, [FromBody]string value)
        {
            var store = new SettingStore(applicationName, version, directory, objectId);
            return Post(store, new SettingModel { Key = key, Value = value });
        }

        [HttpPost]
        [Route("{applicationName}/{version:int=1}/{directory}/{objectId:int=0}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PostCollection(string applicationName, int version, string directory, int objectId, [FromBody]IEnumerable<SettingModel> value)
        {
            var store = new SettingStore(applicationName, version, directory, objectId);
            return Post(store, value);
        }

        public IHttpActionResult Post(SettingStore store, IEnumerable<SettingModel> value)
        {
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


        public IHttpActionResult Post(SettingStore store, SettingModel value)
        {
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
        public IHttpActionResult Get(SettingStore store)
        {
            try
            {
                return Ok<SettingModel[]>(controller.GetSettings(store).ToArray());

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
                return Ok(new SettingModel[] { controller.GetSetting(store, key) });
            } 
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

    }
}