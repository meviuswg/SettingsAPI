using SettingsAPIRepository;
using SettingsAPIRepository.Model;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IHttpActionResult GetDefaultDirectory(string applicationName)
        {
            return GetByDirectory(new SettingStore(applicationName, 1, Constants.DEAULT_DIRECTORY_NAME));
        }

        [HttpPost]
        [Route("{applicationName}/{version}/{directory}/")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult SaveSettings(string applicationName, int version, string directory, [FromBody] IEnumerable<SettingModel> value)
        {
            return SaveSettings(new SettingStore(applicationName, version, directory), value);
        }

        [HttpGet]
        [Route("{applicationName}/{version}/{directory}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult GetByDirectory(string applicationName, int version, string directory)
        {
            return GetByDirectory(new SettingStore(applicationName, version, directory));
        }

        [HttpGet]
        [Route("{applicationName}/{version}/{directory}/{objectId}")]
        [ResponseType(typeof(SettingModel[]))]
        public IHttpActionResult GetByObjectId(string applicationName, int version, string directory, int objectId)
        {
            return GetSetttings(new SettingStore(applicationName, version, directory), objectId);
        }

        [HttpGet]
        [Route("{applicationName}/{version}/{directory}/{objectId}/{key}")]
        [ResponseType(typeof(SettingModel))]
        public IHttpActionResult GetByKey(string applicationName, int version, string directory, int objectId, string key)
        {
            return GetSettting(new SettingStore(applicationName, version, directory), key, objectId);
        }

        [HttpDelete]
        [Route("{applicationName}/{version}/{directory}/{objectId}/{key}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteKey(string applicationName, int version, string directory, int objectId, string key)
        {
            try
            {
                SettingStore store = new SettingStore(applicationName, version, directory);
                controller.DeleteSetting(store, new SettingModel { Key = key, ObjectId = objectId });
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("{applicationName}/{version}/{directory}/{objectId}/{key}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveSetting(string applicationName, int version, string directory, int objectId, string key, [FromBody]string value)
        {
            var store = new SettingStore(applicationName, version, directory);
            return SaveSetting(store, new SettingModel { Key = key, Value = value, ObjectId = objectId });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult SaveSettings(SettingStore store, IEnumerable<SettingModel> value)
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
        public IHttpActionResult SaveSetting(SettingStore store, SettingModel value)
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
        public IHttpActionResult GetByDirectory(SettingStore store)
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
        public IHttpActionResult GetSettting(SettingStore store, string key, int objectId)
        {
            try
            {
                return Ok(controller.GetSetting(store, key, objectId));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult GetSetttings(SettingStore store, int objectId)
        {
            try
            {
                return Ok(controller.GetSettings(store, objectId));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}