using SettingsAPIData;
using SettingsAPIData.Model;
using SettingsAPIShared;
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
    public class ApplicationController : BaseApiController
    {
        private IApplicationRepository controller;
        private ISettingsAuthorizationProvider auth;

        public ApplicationController(IApplicationRepository controller, ISettingsAuthorizationProvider authProvider)
        {
            this.controller = controller;
            this.auth = authProvider;
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
                return Error(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}")]
        [ResponseType(typeof(ApplicationModel))]
        public IHttpActionResult Get(string applicationName)
        {
            try
            {

                var application = controller.GetApplication(applicationName);

                if (application != null)
                    return Ok(application);
                else
                    return NotFound();
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
                var directorties = controller.GetDirectories(applicationName);

                if (directorties != null)
                    return Ok(directorties);
                else
                    return NotFound();

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
                var directorty = controller.GetDirectory(applicationName, directoryName);

                if (directorty != null)
                    return Ok(directorty);
                else
                    return NotFound();

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

                var versions = controller.GetVersions(applicationName);

                if (versions != null)
                    return Ok(versions);
                else
                    return NotFound();

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
                var v = controller.GetVersion(applicationName, version);

                if (v != null)
                    return Ok(v);
                else
                    return NotFound();

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
            if (auth.AllowCreateApplication(applicationName))
            {
                return CreateApplication(new SaveApplicationModel { Name = applicationName });
            }
            else
            {
                return Forbidden();
            }
        }

        [HttpPost]
        [Route("api/application", Order = 1)]
        [ResponseType(typeof(ApplicationModel))]
        public IHttpActionResult CreateApplication([FromBody]SaveApplicationModel value)
        {
            try
            {
                if (auth.AllowCreateApplication(value.Name))
                {
                    var application = controller.CreateApplication(value.Name, value.Description, value.DirectoryName, value.DirectoryDescription);
                    return Ok(application);

                }
                else
                {
                    return Forbidden();
                }
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
                if (auth.AllowDeleteApplication(applicationName))
                {
                    var application = controller.GetApplication(applicationName);

                    if (application != null)
                    {
                        controller.DeleteApplication(applicationName);
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else
                {
                    return Forbidden();
                }
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
                if (auth.AllowCreateDirectory(applicationName, value.Name))
                {
                    controller.CreateDirectory(applicationName, value.Name, value.Description);
                    return Ok();
                }
                else
                {
                    return Forbidden();
                }
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
                if (auth.AllowDeleteDirectory(applicationName, directoryName))
                {
                    controller.DeleteDirectory(applicationName, directoryName);
                    return Ok();
                }
                else
                {
                    return Forbidden();
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }


        [HttpPost]
        [Route("api/application/{applicationName}/versions")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateVersion(string applicationName, int version)
        {
            try
            {
                if (auth.AllowCreateVersion(applicationName))
                {
                    controller.CreateVersion(applicationName, version);
                    return Ok();
                }
                else
                {
                    return Forbidden();
                }
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
                if (auth.AllowDeleteVersion(applicationName))
                {
                    controller.CreateVersion(applicationName, version);
                    return Ok();
                }
                else
                {
                    return Forbidden();
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
