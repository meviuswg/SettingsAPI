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
    public class ApplicationController : BaseApiController
    {
        private IApplicationDataController controller;

        public ApplicationController(IApplicationDataController controller)
        {
            this.controller = controller;
        }

        [HttpGet]
        [Route("api/application/")]
        public IEnumerable<ApplicationModel> Get()
        {
            if (controller.AllowRead())
            {
                return controller.GetApplications();
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}")]
        public ApplicationModel Get(string applicationName)
        {
            if (controller.AllowRead())
            {
                var application = controller.GetApplication(applicationName);

                if (application != null)
                    return application;
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/directories")]
        public IEnumerable<DirectoryModel> GetDirectories(string applicationName)
        {
            if (controller.AllowRead())
            {
                var directories = controller.GetDirectories(applicationName);

                if (directories != null)
                    return directories;
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/directories/{directoryName}")]
        public DirectoryModel GetDirectory(string applicationName, string directoryName)
        {
            if (controller.AllowRead())
            {
                var directory = controller.GetDirectory(applicationName, directoryName);

                if (directory != null)
                    return directory;
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/versions")]
        public IEnumerable<VersionModel> GetVersions(string applicationName)
        {
            if (controller.AllowRead())
            {
                var application = controller.GetApplication(applicationName);

                if (application != null)
                    return application.Versions;
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        [HttpGet]
        [Route("api/application/{applicationName}/versions/{version}")]
        public HttpResponseMessage GetVersion(string applicationName, int version)
        {
            try
            {
                if (controller.AllowRead())
                {
                    var application = controller.GetApplication(applicationName);

                    if (application != null && application.Versions.Count() > 0)
                        return Request.CreateResponse<VersionModel>(application.Versions.SingleOrDefault(v => v.Version == version));
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Application unknown");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            catch (SettingsStoreException ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_MESSAGE_INTERNAL_ERROR);
            }
        }


        [HttpPost]
        [Route("api/application/{applicationName}", Order = 0)]
        public HttpResponseMessage CreateApplication(string applicationName)
        {
            return CreateApplication(new SaveApplicationModel { Name = applicationName });
        }

        [HttpPost]
        [Route("api/application", Order = 1)]
        public HttpResponseMessage CreateApplication([FromBody]SaveApplicationModel value)
        {
            try
            {
                var application = controller.CreateApplication(value.Name, value.Description, value.DirectoryName, value.DirectoryDescription);

                return Request.CreateResponse<ApplicationModel>(application);
            }
            catch (SettingsStoreException ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_MESSAGE_INTERNAL_ERROR);
            }
           
        }


        [HttpDelete]
        [Route("api/application/{applicationName}")]
        public void DeleteApplication(string applicationName)
        {
            if (controller.AllowDelete())
            {
                var application = controller.GetApplication(applicationName);

                if (application != null)
                    controller.DeleteApplication(applicationName);
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        [HttpPost]
        [Route("api/application/{applicationName}/directories/{directoryName}")]
        public HttpResponseMessage CreateDirectory(string applicationName, string directoryName)
        {
            return CreateDirectory(applicationName, new SaveDirectoryModel { Name = directoryName });
        }

        [HttpPost]
        [Route("api/application/{applicationName}/directories")]
        public HttpResponseMessage CreateDirectory(string applicationName, [FromBody] SaveDirectoryModel value)
        {
            try
            {
                controller.CreateDirectory(applicationName, value.Name, value.Description);
                return OkResponse();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_MESSAGE_INTERNAL_ERROR);
            }
        }

        [HttpDelete]
        [Route("api/application/{applicationName}/directories/{directoryName}")]
        public HttpResponseMessage DeleteDirectory(string applicationName, string directoryName)
        {
            try
            {
                controller.DeleteDirectory(applicationName, directoryName);
                return OkResponse();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_MESSAGE_INTERNAL_ERROR);
            }
        }


        [HttpPost]
        [Route("api/application/{applicationName}/versions")]
        public HttpResponseMessage CreateVersion(string applicationName, int version)
        {
            try
            {
                controller.CreateVersion(applicationName, version);
                return OkResponse();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_MESSAGE_INTERNAL_ERROR);
            }
        }

        [HttpDelete]
        [Route("api/application/{applicationName}/versions/{version}")]
        public HttpResponseMessage DeleteVersion(string applicationName, int version)
        {
            try
            {
                controller.CreateVersion(applicationName, version);
                return OkResponse();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_MESSAGE_INTERNAL_ERROR);
            }
        }
    }
}
