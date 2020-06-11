using Exam.Business.Common;
using Exam.DataSource;
using Exam.Entities.Common;
using System;
using System.Collections.Generic;

namespace Exam.Business
{
    public class BssProducts : BusinessBase
    {
        public BssProducts() { }

        public List<Exam.Entities.Product> Retrieve()
        {
            InitalizeMessage();
            if (IsValid)
            {
                return new DbsProduct().Retrive();
            }
            else
            {
                throw new ArgumentException(GetMessage());
            }
        }
    }
}