using Exam.Business;
using Exam.Entities;
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
    }
}
