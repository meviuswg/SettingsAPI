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
        [Route("{applicationName}/{version}/{directory}/")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult SaveSettings(string applicationName, int version, string directory, [FromBody] IEnumerable<SettingModel> value)
        {
            return Post(new SettingStore(applicationName, version, directory), value);
        }

        [HttpGet]
        [Route("{applicationName}/{version}/{directory}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult Get(string applicationName, int version, string directory)
        {
            return Get(new SettingStore(applicationName, version, directory));
        }

        [HttpGet]
        [Route("{applicationName}/{version}/{directory}/{objectId:int=0}")]
        [ResponseType(typeof(SettingModel))]
        public IHttpActionResult Get(string applicationName, int version, string directory, int objectId)
        {

            return Get(new SettingStore(applicationName, version, directory), null, objectId);
        }

        [HttpGet]
        [Route("{applicationName}/{version}/{directory}/{objectId:int=0}/{key}")]
        [ResponseType(typeof(SettingModel))]
        public IHttpActionResult Get(string applicationName, int version, string directory, int objectId,  string key)
        {

            return Get(new SettingStore(applicationName, version, directory), key, objectId);
        }

        [HttpPost]
        [Route("{applicationName}/{version}/{directory}/{objectId:int=0}/{key}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(string applicationName, int version, string directory, int objectId, string key, [FromBody]string value)
        {
            var store = new SettingStore(applicationName, version, directory);
            return Post(store, new SettingModel { Key = key, Value = value, ObjectId = objectId });
        }


        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult Get(SettingStore store, string key)
        {
            return Get(store, key, 0);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult Get(SettingStore store, string key, int objectId)
        {
            try
            {
                return Ok(new SettingModel[] { controller.GetSetting(store, key, objectId) });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

    }
}