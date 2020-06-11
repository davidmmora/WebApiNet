using Exam.Business;
using Exam.Entities;
using System;
using System.Web.Http;
using WebApi.Common;

namespace WebApi.Controllers
{
    public class ProductController : ApiControllerBase
    {
        [HttpPost]
        [Route(template: "api/Product/list")]
        public IHttpActionResult List()
        {
            try
            {
                var model = new BssProducts().Retrieve();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ExMessage(ex));
            }
        }
    }
}
