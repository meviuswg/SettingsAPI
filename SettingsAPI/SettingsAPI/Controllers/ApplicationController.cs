using SettingsAPIData;
using SettingsAPIData.Model;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace SettingsAPI.Controllers
{
    [Authorize]
    public class ApplicationController : BaseApiController
    {
        private IApplicationRepository controller;

        public ApplicationController(IApplicationRepository controller)
        {
            this.controller = controller;
        }

        [HttpGet]
        [Route("api/application/")]
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

        [HttpGet]
        [Route("api/application/{applicationName}")]
        [ResponseType(typeof(ApplicationModel))]
        public IHttpActionResult Get(string applicationName)
        {
            try
            {
                return Ok(controller.GetApplication(applicationName));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/directories")]
        [ResponseType(typeof(DirectoryModel[]))]
        public IHttpActionResult GetDirectories(string applicationName)
        {
            try
            {
                return Ok(controller.GetDirectories(applicationName));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/directories/{directoryName}")]
        [ResponseType(typeof(DirectoryModel))]
        public IHttpActionResult GetDirectory(string applicationName, string directoryName)
        {
            try
            {
                return Ok(controller.GetDirectory(applicationName, directoryName));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/versions")]
        [ResponseType(typeof(VersionModel[]))]
        public IHttpActionResult GetVersions(string applicationName)
        {
            try
            {
                return Ok(controller.GetVersions(applicationName));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/versions/{version}")]
        [ResponseType(typeof(VersionModel))]
        public IHttpActionResult GetVersion(string applicationName, int version)
        {
            try
            {
                return Ok(controller.GetVersion(applicationName, version));
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("api/application/{applicationName}", Order = 0)]
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
        [Route("api/application", Order = 1)]
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
        [Route("api/application/{applicationName}")]
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

        [HttpPost]
        [Route("api/application/{applicationName}/directories/{directoryName}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateDirectory(string applicationName, string directoryName)
        {
            return CreateDirectory(applicationName, new SaveDirectoryModel { Name = directoryName });
        }

        [HttpPost]
        [Route("api/application/{applicationName}/directories")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateDirectory(string applicationName, [FromBody] SaveDirectoryModel value)
        {
            try
            {
                controller.CreateDirectory(applicationName, value.Name, value.Description);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpDelete]
        [Route("api/application/{applicationName}/directories/{directoryName}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteDirectory(string applicationName, string directoryName)
        {
            try
            {
                controller.DeleteDirectory(applicationName, directoryName);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        [Route("api/application/{applicationName}/versions/{version}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateVersion(string applicationName, int version)
        {
            try
            {
                controller.CreateVersion(applicationName, version);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpDelete]
        [Route("api/application/{applicationName}/versions/{version}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteVersion(string applicationName, int version)
        {
            try
            {
                controller.DeleteVersion(applicationName, version);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}