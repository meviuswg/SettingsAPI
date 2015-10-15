using SettingsAPIData;
using SettingsAPIData.Model;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace SettingsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/admin")]
    public class AdminController : BaseApiController
    {
        private IApplicationRepository controller;
        private IApiKeyRepository keyController;

        public AdminController(IApplicationRepository controller, IApiKeyRepository keyController)
        {
            this.controller = controller;
            this.keyController = keyController;
        }

        #region Applications

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(ApplicationModel[]))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(controller.GetApplications());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("{applicationName}")]
        [ResponseType(typeof(ApplicationModel))]
        public IHttpActionResult CreateApplication(string applicationName)
        {
            try
            {
                return CreateApplication(new SaveApplicationModel { Name = applicationName });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(ApplicationModel))]
        public IHttpActionResult CreateApplication([FromBody]SaveApplicationModel value)
        {
            try
            {
                return Ok(controller.CreateApplication(value.Name, value.Description, value.DirectoryName, value.DirectoryDescription));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpDelete]
        [Route("{applicationName}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteApplication(string applicationName)
        {
            try
            {
                controller.DeleteApplication(applicationName);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        #endregion

        #region ApiKey

        [HttpGet]
        [Route("{applicationName}/apikeys")]
        [ResponseType(typeof(ApiKeyModel[]))]
        public IHttpActionResult GetApiKeys(string applicationName)
        {
            try
            {
                return Ok(keyController.GetApplicationApiKeys(applicationName));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        #endregion
    }
}