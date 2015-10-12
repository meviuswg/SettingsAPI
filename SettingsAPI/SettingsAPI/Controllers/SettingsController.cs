using SettingsAPIData;
using SettingsAPIData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [ResponseType(typeof(SettingModel[]))]
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
                if (controller.Exists(store))
                {
                    if (controller.AllowRead(store))
                    {
                       return Ok(controller.GetSettings(store));
                    }
                    else
                    {
                        return Forbidden();
                    }
                }
                else
                {
                    return NotFound();
                }
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
                if (controller.Exists(store))
                {
                    if (controller.AllowRead(store))
                    {
                        return Ok(controller.GetSetting(store, key));
                    }
                    else
                    {
                        return Forbidden();
                    }
                }
                else
                {
                    return NotFound();
                }
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
            SettingModel model = new SettingModel { Key = key, ObjectId = objectId, Value = value };
            return Post(applicationName, version, directory, new[] { model });
        }

        [HttpPost]
        [Route("api/settings/{applicationName}/{version}/{directory}/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(string applicationName, int version, string directory, [FromBody]IEnumerable<SettingModel> value)
        {
            var store = new SettingStore(applicationName, version, directory);

            try
            {
                if (controller.Exists(store))
                {
                    if (controller.AllowWrite(store))
                    {
                        try
                        {
                            controller.SaveSettings(store, value);
                        }
                        catch (SettingsAuthorizationException ex)
                        {
                            return Forbidden(ex.Message);
                        }

                        return Ok();
                    }
                    else
                    {
                        return Forbidden();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}