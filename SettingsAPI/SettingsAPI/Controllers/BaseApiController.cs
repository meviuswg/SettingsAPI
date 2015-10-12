using SettingsAPIData;
using SettingsAPIShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SettingsAPI.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected HttpResponseMessage OkResponse()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected HttpResponseMessage OkResponse(object model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        protected HttpResponseMessage ForbiddenReponse()
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        protected HttpResponseMessage ErrorReponse()
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Constants.ERROR_INTERNAL_ERROR);
        }

        protected HttpResponseMessage ErrorReponse(string message)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        }

        protected HttpResponseMessage NotFoundReponse()
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        protected HttpResponseMessage NotFoundReponse(object model)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, model);
        }

        protected IHttpActionResult Forbidden(string message)
        {
            return Content<string>(HttpStatusCode.Forbidden, message);
        }

        protected IHttpActionResult Forbidden()
        {
            return Content<string>(HttpStatusCode.Forbidden, Constants.ERROR_FORBIDDEN);
        }

        protected IHttpActionResult Error()
        {
            return Content<string>(HttpStatusCode.InternalServerError, Constants.ERROR_INTERNAL_ERROR);
        }

        protected IHttpActionResult Error(string message)
        {
            return Content<string>(HttpStatusCode.InternalServerError, message);
        }

        protected IHttpActionResult Error(Exception ex)
        {
            Log.Exception(ex);

            string message = Constants.ERROR_INTERNAL_ERROR;

            if(ex is SettingsStoreException)
            {
                message = ex.Message;
            }
            return Content<string>(HttpStatusCode.InternalServerError, message);
        }

    }
}
