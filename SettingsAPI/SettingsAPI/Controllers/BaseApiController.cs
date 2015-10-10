using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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

        protected HttpResponseMessage NotFoundReponse()
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        protected HttpResponseMessage NotFoundReponse(object model)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound, model);
        }


    }
}
