using SettingsAPIData;
using SettingsAPIData.Model;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace SettingsAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/application")]
    public class ApplicationController : BaseApiController
    {
        private IApplicationRepository controller;

        public ApplicationController(IApplicationRepository controller)
        {
            this.controller = controller;
        }

        #region Application 

        [HttpGet]
        [Route("{applicationName}")]
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

        #endregion Application

        #region Directory

        [HttpPost]
        [Route("{applicationName}/directories/{directoryName}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateDirectory(string applicationName, string directoryName)
        {
            return CreateDirectory(applicationName, new SaveDirectoryModel { Name = directoryName });
        }

        [HttpPost]
        [Route("{applicationName}/directories")]
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
        [Route("{applicationName}/directories/{directoryName}")]
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

        [HttpGet]
        [Route("{applicationName}/directories")]
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
        [Route("{applicationName}/directories/{directoryName}")]
        [ResponseType(typeof(DirectoryModel))]
        public IHttpActionResult GetDirectory(string applicationName, string directoryName)
        {
            try
            {
                return Ok(controller.GetDirectories(applicationName, directoryName).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        #endregion Directory

        #region Version

        [HttpPost]
        [Route("{applicationName}/versions/{version}")]
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
        [Route("{applicationName}/versions/{version}")]
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

        [HttpGet]
        [Route("{applicationName}/versions/{version}")]
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

        [HttpGet]
        [Route("{applicationName}/versions")]
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

        #endregion Version 
    }
}