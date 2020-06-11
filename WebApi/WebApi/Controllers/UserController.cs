using Exam.Business;
using Exam.Entities;
using System;
using System.Web.Http;
using WebApi.Common;

namespace WebApi.Controllers
{
    public class UserController : ApiControllerBase
    {

        [HttpGet]
        [Route(template: "api/User/Get")]
        public Exam.Entities.User Get(string filter)
        {
            var model = new BssUsers().Retrieve(filter);
            return model;
        }

        [HttpPost]
        [Route(template: "api/User/Auth")]
        public IHttpActionResult Post(Exam.Entities.User data)
        {
            try
            {
                var model = new BssUsers().AuthenticatePath(data);
                return Ok(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ExMessage(ex));
            }
        }
    }
}
