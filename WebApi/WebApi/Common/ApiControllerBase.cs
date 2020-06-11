using Exam.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Common
{
    public class ApiControllerBase : ApiController
    {
        protected string ExMessage(Exception ex)
        {
            var message = ex.Message;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                message += $"{ex.Message} {Environment.NewLine}";
            }
            return message;
        }
    }
}